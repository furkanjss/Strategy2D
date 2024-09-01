using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class BarracksModel: BuildingModel
    {
       
        private List<SoldierData> SoldierDatas = new List<SoldierData>();
        public BarracksModel(BuildingData buildingData) : base(buildingData)
        {
            SoldierDatas = buildingData.SoldierDatas;
        }

        public  List<SoldierData> GetSoldierDatas()
        {
            return SoldierDatas;
        }
    }
}