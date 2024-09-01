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
                Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, interactableLayer);

                if (hit.collider != null)
                {
                 
                    IInteractable interactable = hit.transform.GetComponent<IInteractable>();
                    if (interactable!=null)
                    {
                        if (interactable.CanMove())
                        {
                            _currentInteractable=interactable;
                        }
                        else
                        {
                            interactable.SetInformationToPanel();
                        }
                     
                    }
                    _currentDraggable = hit.transform.GetComponent<IDraggable>();
                    if (_currentDraggable != null)
                    {
                        
                        _currentInteractable = null;
                        _isDragging = true;
                    }
                    if (_currentDraggable==null&&_currentInteractable!=null)
                    {
                        if (hit.collider.gameObject.GetComponent<GridPiece>()!=null&&hit.collider.gameObject.GetComponent<GridPiece>().IsAvailable)
                        {
                            _currentInteractable.SetTargetGrid(hit.collider.gameObject.GetComponent<GridPiece>());
                            _currentInteractable = null;
                        }
                    }
                }
            }

            if (_isDragging)
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mainCamera.nearClipPlane));
                    mousePosition.z = _mainCamera.WorldToScreenPoint(transform.position).z;
                    _currentDraggable?.OnDrag(mousePosition);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mainCamera.nearClipPlane));
                    mousePosition.z = _mainCamera.WorldToScreenPoint(transform.position).z;
                    _isDragging = false;
                    _currentDraggable?.OnDragEnd(mousePosition);
                    _currentDraggable = null;
                }
            }
        }
    }
    
}