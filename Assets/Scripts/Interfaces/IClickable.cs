using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IInteractable
    {
        public bool CanMove();

        public void SetTargetGrid(GridPiece targetGrid)
        {
            
        }

        public void SetInformationToPanel();

    }
}