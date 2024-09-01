using System;
using Factory;
using Interfaces;
using Models;
using UnityEngine;
using UnityEngine.EventSystems;
using Views;

namespace Controllers
{
   public class BuildingController : BaseController<BuildingModel, BuildingView>, IDraggable
{
    [SerializeField] private BuildingData _buildingData;
    private Camera mainCamera;
    private IBuildingFactory _buildingFactory;
    protected GridPiece currentGrid;

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

    public void OnDrag(Vector3 position)
    {
        if (_model.GetStatus() == BuildingStatus.Available)
        {
            transform.position = position;
        }
    }
    public override void OnDeath()
    {
        currentGrid.ClearGrid();
        Destroy(gameObject);

    }
    public void OnDragEnd(Vector3 position)
    {
        if (_model.GetStatus() == BuildingStatus.Available)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;

            GetCollidedGrid(mousePosition);

            if (currentGrid != null && currentGrid.IsPossiblePlaceObject(_model.Size))
            {
                PlaceBuilding();
            }
            else
            {
                _view.HighlightInvalidPlacement();
                Debug.Log("No Empty Place For Building " + _model.GetStatus());
            }
        }
        else
        {
            SetInformation();
        }
    }

    private void GetCollidedGrid(Vector3 mousePosition)
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
    }
}
