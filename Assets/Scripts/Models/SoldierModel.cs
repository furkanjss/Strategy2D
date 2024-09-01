using System;
using UnityEngine;

namespace Models
{
    public class SoldierModel
    {
        public event Action<float> OnHealthChanged;

        private float _health;
        private readonly float _maxHealth;
        private readonly float _damage;
        private readonly Sprite _soldierSprite;
        #region Encapsulation
        private GridPiece currentGrid;

        private GridPiece _currentGrid;
        private string _name;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public GridPiece CurrentGrid
        {
            get => _currentGrid;
            set => _currentGrid = value;
        }

        #endregion
        public SoldierModel(SoldierData soldierData)
        {
            _maxHealth = soldierData.health;
            _health = _maxHealth;
            _damage = soldierData.damage;
            _name  = soldierData.soldierName;
            _soldierSprite = soldierData.soldierSprite;
        }
        public Sprite GetSoldierSprite() => _soldierSprite;

        public float GetHealth() => _health;
        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health < 0) _health = 0;
            OnHealthChanged?.Invoke(_health);
        }
        public float GetDamage() => _damage;

       
    }
}