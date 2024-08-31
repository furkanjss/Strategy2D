using System;
using Factory;
using Interfaces;
using Models;
using UnityEngine;
using UnityEngine.EventSystems;
using Views;

namespace Controllers
{
    public class BuildingControler: MonoBehaviour,IDraggable
    {
        [SerializeField] private BuildingData _buildingData;
        protected BuildingModel _model;
        protected BuildingView _view;
        protected Camera mainCamera;
        private IBuildingFactory _buildingFactory;
        protected GridPiece currentGrid;
        private Vector3 offset;
        protected virtual void Start()
        {
            mainCamera = Camera.main;
            var factoryResolver = new BuildingFactoryResolver();
            _buildingFactory = factoryResolver.GetFactory(_buildingData.buildingType);
            _view =transform.GetChild(0).GetComponent<BuildingView>();
            BuildingModel model = _buildingFactory.CreateModel(_buildingData);
            Initialize(model, _view);
        }

        protected void Initialize(BuildingModel model, BuildingView view)
        {
            _model = model;
            _view = view;
            _view.Initialize(_model);
        }

        public void PlaceBuilding()
        {
            if (_model.GetStatus()==BuildingStatus.Available)
            {
                currentGrid.SetObjectOnGrid(gameObject,_model.Size);
                _model.ChangeStatus(BuildingStatus.Placed);
            }
        }

        private void OnMouseDown()
        {
            if(_model.GetStatus()==BuildingStatus.Placed)
            {
                print("sss");
            }
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

       
    }
}