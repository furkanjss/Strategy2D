using Models;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building Data")]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    public Sprite buildImage;
    public BuildType buildingType;
    public GameObject buildingPrefab;       
    public Vector2Int sizeBuilding;         
    public int health;
    public BuildingModel buildingModel;
}

public enum BuildType
{
    Barracks,PowerPlant 
}