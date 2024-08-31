using System;
using Models;

namespace Factory
{
    public class BuildingFactory
    {
        public static BuildingModel CreateModel(BuildType type, BuildingData data)
        {
            switch (type)
            {
                case BuildType.Barracks:
                    return new BarracksModel(data);
                case  BuildType.PowerPlant:
                    return new PowerPlantModel(data);
                default:
                    throw new ArgumentException("Unsupported Building Type");
            }
        }
    }
}