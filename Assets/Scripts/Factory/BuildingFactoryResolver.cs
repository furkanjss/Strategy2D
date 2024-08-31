using System;
using System.Collections.Generic;

namespace Factory
{
    public class BuildingFactoryResolver
    {
        private readonly Dictionary<BuildType, IBuildingFactory> _factories;

        public BuildingFactoryResolver()
        {
            _factories = new Dictionary<BuildType, IBuildingFactory>
            {
                { BuildType.Barracks, new BarracksFactory() },
                { BuildType.PowerPlant, new PowerPlantFactory() }
            };
        }

        public IBuildingFactory GetFactory(BuildType type)
        {
            if (_factories.TryGetValue(type, out var factory))
            {
                return factory;
            }

            throw new ArgumentException("Unsupported Building Type");
        }
    }
}