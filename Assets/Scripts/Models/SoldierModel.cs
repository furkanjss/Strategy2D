using System;
using UnityEngine;

namespace Models
{
    public class SoldierModel
    {
        public event Action<float> OnHealthChanged;

        private float _health;
        private float _maxHealth;
        private float _damage;
        private Sprite soldierSprite;
        #region Encapsulation

        private string name;
        public string Name
        {
            get => name;
            set => name = value;
        }

        #endregion
        public SoldierModel(SoldierData soldierData)
        {
            _maxHealth = soldierData.health;
            _health = _maxHealth;
            _damage = soldierData.damage;
            name = soldierData.soldierName;
            soldierSprite = soldierData.soldierSprite;
        }
        public Sprite GetSoldierSprite() => soldierSprite;

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