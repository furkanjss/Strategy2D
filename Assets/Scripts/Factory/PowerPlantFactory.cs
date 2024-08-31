using Models;


namespace Factory
{
    public class PowerPlantFactory : IBuildingFactory
    {
        public BuildingModel CreateModel(BuildingData data)
        {
            return new PowerPlantModel(data);
        }
    }
}