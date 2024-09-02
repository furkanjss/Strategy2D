using System;

using UnityEngine;

namespace Utilities
{
    public class InputManager:MonoBehaviour
    {
         private Camera _mainCamera;
        [SerializeField] private LayerMask interactableLayer;
        private GridPiece currentGrid;
        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                ProcessMouseDown();
            }
            if (Input.GetMouseButtonDown(1)) 
            {
                HandleRightClick();
            }
           
        }

        private void ProcessMouseDown()
        {
            
            Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, interactableLayer);

            if (hit.collider == null) return;
            HandleInteractable(hit.transform);

        }

        private void HandleInteractable(Transform transform)
        {
            GridPiece interactable = transform.GetComponent<GridPiece>();
            if (interactable == null) return;
            interactable.SetInformationToPanel();
            if (interactable == currentGrid) return;
            if (interactable.ContainsSoldier( ))
            {
                currentGrid = interactable;
            }

        }
        
        private void HandleGridPieceInteraction(Transform transform)
        {
            GridPiece targetGrid = transform.GetComponent<GridPiece>();
            if (targetGrid == null) return;
            if (currentGrid == targetGrid) return;
            if (currentGrid == null) return;

            if (targetGrid.IsAvailable)
            {
                currentGrid.SetTargetGrid(targetGrid,false);
                currentGrid = null;
                InformationPanel.RaiseOnInformationCleared();
            }
            else
            {
              
                currentGrid.SetTargetGrid(targetGrid, true);
                currentGrid = null;
                InformationPanel.RaiseOnInformationCleared();

             
            }
        }

        private void HandleRightClick()
        {
            Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, interactableLayer);

            if (hit.collider == null) return;

            HandleGridPieceInteraction(hit.transform);
        }
        
    
    }
}
    
