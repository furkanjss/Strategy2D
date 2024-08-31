using Models;

namespace Factory
{
    public interface IBuildingFactory
    {
        BuildingModel CreateModel(BuildingData data);
    }

}