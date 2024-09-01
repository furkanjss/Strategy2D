using System;
using UnityEngine;

namespace Models
{
   public class BuildingModel : BaseModel
{
    public event Action<BuildingStatus> OnStatusChanged;

    private BuildingStatus _buildingStatus;
    private Vector2Int _size;
    private BuildType _buildType;

    public Vector2Int Size
    {
        get => _size;
        set => _size = value;
    }

    public BuildType GetBuildType() => _buildType;

    public BuildingModel(BuildingData buildingData)
        : base(buildingData.health, buildingData.buildImage, buildingData.buildingName)
    {
        _size = buildingData.sizeBuilding;
        _buildType = buildingData.buildingType;
        ChangeStatus(BuildingStatus.Available);
    }

    public void ChangeStatus(BuildingStatus status)
    {
        if (_buildingStatus != status)
        {
            _buildingStatus = status;
            OnStatusChanged?.Invoke(status);
        }
    }
  
    public BuildingStatus GetStatus() => _buildingStatus;
}
    
    public enum BuildingStatus
    {
        Available,
        Placed
    }
}