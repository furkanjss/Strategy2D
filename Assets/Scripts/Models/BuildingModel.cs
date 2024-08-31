using System;
using UnityEngine;

namespace Models
{
    public class BuildingModel
    {
        public event Action<BuildingStatus> OnStatusChanged;
        public event Action<float> OnHealthChanged;

        private BuildingStatus _buildingStatus;
        private float _health;
        private float _maxHealth;
        private Vector2Int _size;
        public Vector2Int Size
        {
            get { return _size; }
            set { _size = value; }
        }        private GameObject _buildingPrefab;
        private Sprite _buildingSprite;

        public BuildingModel(BuildingData buildingData)
        {
            _buildingPrefab = buildingData.buildingPrefab;
            _buildingSprite = buildingData.buildImage;
            _maxHealth = buildingData.health;
            _health = _maxHealth;
            _size = buildingData.sizeBuilding;
            ChangeStatus(BuildingStatus.Available);
        }

        public Sprite GetBuildingSprite() => _buildingSprite;

        public void ChangeStatus(BuildingStatus status)
        {
            if (_buildingStatus != status)
            {
                _buildingStatus = status;
                OnStatusChanged?.Invoke(status);
            }
        }

        public GameObject GetBuildPrefab() => _buildingPrefab;

        public BuildingStatus GetStatus() => _buildingStatus;

        public float GetHealth() => _health;
        
        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health < 0) _health = 0;
            OnHealthChanged?.Invoke(_health);
        }

        public void Heal(float amount)
        {
            _health += amount;
            if (_health > _maxHealth) _health = _maxHealth;
            OnHealthChanged?.Invoke(_health);
        }

    }
    
    public enum BuildingStatus
    {
        Available,
        Placed
    }
}