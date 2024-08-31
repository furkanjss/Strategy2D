
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class DragHandler : MonoBehaviour
    {
        
        private Vector3 offset;
       
        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }
        

        private void OnMouseDown()
        {
            // Dünya koordinatlarını ekran koordinatlarına çevirir
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
            offset = transform.position - mainCamera.ScreenToWorldPoint(mousePosition);
        }

        private void OnMouseDrag()
        {
            // Ekran koordinatlarını dünya koordinatlarına çevirir
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
            transform.position = mainCamera.ScreenToWorldPoint(mousePosition) + offset;
        }
    }

