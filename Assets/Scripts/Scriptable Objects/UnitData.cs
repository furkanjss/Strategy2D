using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit Data")]
public class UnitData : ScriptableObject
{
    public string unitType;           
    public Sprite unitImage;            
    public int healthPoints;           
    public int damage;                  
}

