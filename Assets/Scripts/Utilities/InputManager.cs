using System;
using Interfaces;
using UnityEngine;

namespace Utilities
{
    public class InputManager:MonoBehaviour
    {
        private Camera mainCam;
        public GameObject selectedObject;
        Vector3 offset;
        private void Start()
        {
            mainCam=Camera.main;
            
        }

        void Update()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

                if (targetObject.GetComponent<IDraggable>()!=null&&targetObject!=null)
                {
                    selectedObject = targetObject.transform.gameObject;
                    offset = selectedObject.transform.position - mousePosition;
                }
            }

            if (selectedObject)
            {
                selectedObject.transform.position = mousePosition + offset;
            }

            if (Input.GetMouseButtonUp(0) && selectedObject)
            {
                selectedObject = null;
            }
        }
        private void GetClickedObject()
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                IDraggable ıDraggable = hit.collider.gameObject.GetComponent<IDraggable>();
                if (ıDraggable != null)
                {
                    // Object has the Interactable script, call its interaction method
                   print(hit.collider.gameObject);
                }
            }
        }
        private void HandleMouseInput()
        {
            if (Input.GetMouseButton(0))
            {
               
            }
            Vector3 mousePosition = Input.mousePosition;
            Debug.Log("Mouse Position: " + mousePosition);
        }

      
    }
    
}