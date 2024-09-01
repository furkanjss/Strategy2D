using System;
using Interfaces;
using UnityEngine;

namespace Utilities
{
    public class InputManager:MonoBehaviour
    {
        private Camera _mainCamera;
        private IDraggable _currentDraggable;
        private IInteractable _currentInteractable;
        private bool _isDragging;
        [SerializeField] private LayerMask interactableLayer;

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

        if (_isDragging)
        {
            HandleDragging();
        }
    }

    private void ProcessMouseDown()
    {
        Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, interactableLayer);

        if (hit.collider == null) return;

        HandleInteractable(hit.transform);
        HandleDraggable(hit.transform);
    }

    private void HandleInteractable(Transform transform)
    {
        IInteractable interactable = transform.GetComponent<IInteractable>();
        if (interactable == null) return;

        if (interactable.CanMove())
        {
            interactable.SetInformationToPanel();
            _currentInteractable = interactable;
        }
        else
        {
            interactable.SetInformationToPanel();
        }
    }

    private void HandleDraggable(Transform transform)
    {
        _currentDraggable = transform.GetComponent<IDraggable>();

        if (_currentDraggable != null)
        {
            _currentInteractable = null;
            _isDragging = true;
        }
        else if (_currentInteractable != null)
        {
            HandleGridPieceInteraction(transform);
        }
    }

    private void HandleGridPieceInteraction(Transform transform)
    {
        GridPiece gridPiece = transform.GetComponent<GridPiece>();
        if (gridPiece == null) return;

        if (gridPiece.IsAvailable)
        {
         
        }
        else
        {
           
        }
        
        _currentInteractable.SetTargetGrid(gridPiece);
        _currentInteractable = null;
    }
    private void HandleDragging()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            _currentDraggable?.OnDrag(mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            _isDragging = false;
            _currentDraggable?.OnDragEnd(mousePosition);
            _currentDraggable = null;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = _mainCamera.WorldToScreenPoint(transform.position).z;
        return mousePosition;
    }
    }
    
}