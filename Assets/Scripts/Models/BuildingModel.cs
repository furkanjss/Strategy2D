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
        private GameObject _buildingPrefab;
        private Sprite _buildingSprite;
  
        private BuildType buildType;

        #region Encapsulation

        private Vector2Int size;
        private string name; 

        #endregion
      
        public Vector2Int Size
        {
            get => size;
            set => size = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        } 

        public BuildingModel(BuildingData buildingData)
        {
            _buildingPrefab = buildingData.buildingPrefab;
            _buildingSprite = buildingData.buildImage;
            _maxHealth = buildingData.health;
            _health = _maxHealth;
            size = buildingData.sizeBuilding;
            name = buildingData.buildingName;
            buildType = buildingData.buildingType;
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

        public BuildType GetBuildType() => buildType;


    }
    
    public enum BuildingStatus
    {
        Available,
        Placed
    }
}