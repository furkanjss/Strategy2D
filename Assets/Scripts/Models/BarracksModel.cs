using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class BarracksModel: BuildingModel
    {
        public event Action OnSoldierProduced;
        private GameObject _soldierPrefab;
        private List<SoldierData> SoldierDatas = new List<SoldierData>();
        public BarracksModel(BuildingData buildingData) : base(buildingData)
        {
            SoldierDatas = buildingData.SoldierDatas;
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
            return Vector3.zero;
        }

        public  List<SoldierData> GetSoldierDatas()
        {
            return SoldierDatas;
        }
    }
}