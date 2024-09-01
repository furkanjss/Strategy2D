using System;
using UnityEngine;

namespace Models
{
    public class SoldierModel:BaseModel
    {
        private readonly float _damage;
        private GridPiece _currentGrid;

        public GridPiece CurrentGrid
        {
            get => _currentGrid;
            set => _currentGrid = value;
        }

        public SoldierModel(SoldierData soldierData)
            : base(soldierData.health, soldierData.soldierSprite, soldierData.soldierName)
        {
            _damage = soldierData.damage;
        }

        public float GetDamage() => _damage;

       
    }
}