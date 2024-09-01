using Unity.VisualScripting;
using UnityEngine;
using System;
namespace Models
{
    public abstract class BaseModel
    {
        public event Action<float> OnHealthChanged;

        protected float _health;
        protected readonly float _maxHealth;
        protected readonly Sprite _sprite;
        protected string _name;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public BaseModel(float maxHealth, Sprite sprite, string name)
        {
            _maxHealth = maxHealth;
            _health = _maxHealth;
            _sprite = sprite;
            _name = name;
        }

        public Sprite GetSprite() => _sprite;

        public float GetHealth() => _health;

        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health < 0) _health = 0;
            OnHealthChanged?.Invoke(_health);
        }
    }
    

}