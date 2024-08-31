using System;
using UnityEngine;

namespace Models
{
    public class BarracksModel: BuildingModel
    {
        public event Action OnSoldierProduced;
        private GameObject _soldierPrefab;

        public BarracksModel(BuildingData buildingData) : base(buildingData)
        {
            //_soldierPrefab = buildingData.soldierPrefab;
        }

        public void ProduceSoldier()
        {
            if (_soldierPrefab != null)
            {
              //  Instantiate(_soldierPrefab, GetRandomPosition(), Quaternion.identity);
                OnSoldierProduced?.Invoke();
            }
        }

        private Vector3 GetRandomPosition()
        {
            // Askerin spawn edileceği rasgele bir konum belirleyin
            return Vector3.zero; // Örnek olarak sıfır vektörü döndürüyoruz
        }
    }
}