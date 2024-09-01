using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IInteractable
    {
        void SetInformationToPanel();
        void SetTargetGrid(GridPiece gridPiece, bool attack);
        bool CanMove();
        GridPiece GetGrid();

    }
}