using UnityEngine;

[CreateAssetMenu(fileName = "SoldierData", menuName = "Game/SoldierData", order = 0)]
public class SoldierData : ScriptableObject
{
    public string soldierName;
    public float health;
    public float damage;
    public Sprite soldierSprite;
    public GameObject soldierPrefab;
}