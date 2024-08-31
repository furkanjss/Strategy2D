using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionModel
{
  public event Action<ProductionStatus> OnStatusChanged;
  private ProductionStatus _productionStatus;
  private BuildType _buildType;
  private GameObject _buildPrefab;
  private Sprite _productionSprite;

  public ProductionModel(BuildingData buildingData)
  {
    _buildType = buildingData.buildingType;
    _buildPrefab = buildingData.buildingPrefab; 
    _productionSprite = buildingData.buildImage;
    ChangeStatus(ProductionStatus.Full);
  }

  public Sprite GetProductionSprite() => _productionSprite;

  public void ChangeStatus(ProductionStatus status)
  {
    if (_productionStatus != status)
    {
      _productionStatus = status;
      OnStatusChanged?.Invoke(status);
    }
  }
  public GameObject GetBuildPrefab() => _buildPrefab;

  public ProductionStatus GetStatus() => _productionStatus;
}

public enum ProductionStatus
{
  Empty,
  Full 
}