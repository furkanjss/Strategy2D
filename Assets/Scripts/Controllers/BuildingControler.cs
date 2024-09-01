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
                    currentGrid.SetBuildingOnGrid(gameObject,_model.Size);
                    _model.ChangeStatus(BuildingStatus.Placed);
                    ChangeLayer();
            }
        }

        void ChangeLayer()
        {
            int newLayer = LayerMask.NameToLayer( "Ignore Raycast");
            gameObject.layer =newLayer;
        }


        public void SetInformation()
        {
            if(_model.GetStatus()==BuildingStatus.Placed)
            {
                InformationPanel.RaiseOnInformationSet(_model);
                print(_model);
            }
        }
     
   
        public void OnDrag(Vector3 position)
        {
            if (_model.GetStatus() == BuildingStatus.Available)
            {
                transform.position = position;
            }
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
                    print("No Empty Place For Building " + _model.GetStatus());
                }
            }
            else
            {
                 SetInformation();
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
        
    }
}