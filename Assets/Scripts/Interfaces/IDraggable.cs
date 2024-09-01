using UnityEngine;

namespace Interfaces
{
    public interface IDraggable
    {
        void OnDrag(Vector3 position);
        void OnDragEnd(Vector3 mousePosition);
    }
}