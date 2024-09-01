using System;
using Factory;

using Models;
using UnityEngine;
using UnityEngine.EventSystems;
using Views;

namespace Controllers
{
   public class BuildingController : BaseController<BuildingModel, BuildingView>
{
    [SerializeField] private BuildingData _buildingData;
    private Camera mainCamera;
    private IBuildingFactory _buildingFactory;
    protected GridPiece currentGrid;
    private Vector3 offset;
    protected override void Start()
    {
        mainCamera = Camera.main;
        var factoryResolver = new BuildingFactoryResolver();
        _buildingFactory = factoryResolver.GetFactory(_buildingData.buildingType);
        base.Start(); 
    }

    protected override BuildingModel CreateModel()
    {
        return _buildingFactory.CreateModel(_buildingData);
    }

    public void PlaceBuilding()
    {
        if (_model.GetStatus() == BuildingStatus.Available)
        {
            currentGrid.SetBuildingOnGrid(gameObject, _model.Size);
            _model.ChangeStatus(BuildingStatus.Placed);
            ChangeLayer();
            GetComponent<Collider2D>().enabled = false;
            Destroy(GetComponent<Rigidbody>());
        }
    }

    private void ChangeLayer()
    {
        int newLayer = LayerMask.NameToLayer("Ignore Raycast");
        gameObject.layer = newLayer;
    }
    

    private void OnMouseUp()
    {
        if (_model.GetStatus() == BuildingStatus.Available)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
            offset = transform.position - mainCamera.ScreenToWorldPoint(mousePosition);

            GetCollidedGrid(mousePosition);
             
            if (currentGrid != null&&currentGrid.IsPossiblePlaceObject(_model.Size))
            {
                PlaceBuilding();
            }
            else
            {
                print("No Empty Place For Building"+  _model.GetStatus());
                  _view.HighlightInvalidPlacement();
            }
        }
    }

    void GetCollidedGrid(Vector3 mousePosition)
    {
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        worldMousePosition.z = 0; 
        float radius = 0.5f; 
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(worldMousePosition, radius);

        foreach (Collider2D collider in hitColliders)
        {
            GridPiece gridPiece = collider.GetComponent<GridPiece>();
            if (gridPiece != null)
            {
                currentGrid = gridPiece;
                break;
            }
        }

    }
    private void OnMouseDrag()
    {
        if (_model.GetStatus() == BuildingStatus.Available)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
            transform.position = mainCamera.ScreenToWorldPoint(mousePosition) + offset;
        }
          
    }
    public override void OnDeath()
    {
        currentGrid.ClearOccupiedGrids();
        Destroy(gameObject);

    }

    
    }
}
