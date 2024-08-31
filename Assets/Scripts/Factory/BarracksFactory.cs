using Models;


namespace Factory
{
    public class BarracksFactory : IBuildingFactory
    {
        public BuildingModel CreateModel(BuildingData data)
        {
            return new BarracksModel(data);
        }
    }

}