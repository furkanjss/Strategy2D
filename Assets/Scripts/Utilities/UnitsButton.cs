using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitsButton : MonoBehaviour
{
    private SoldierData soldierData;
    private Button unitButton;
    
    void Start()
    {
        unitButton = GetComponent<Button>();
        unitButton.onClick.AddListener(CreateUnit);
    }

    public void GetSoldierData(SoldierData soldierData)
    {
        this.soldierData = soldierData;
    }
    void CreateUnit()
    {
        GridPiece targetGrid = GridManager.Instance.FindEmptyGrid();
        if (targetGrid!=null)
        {
            targetGrid.SetSoldierOnGrid();
            Instantiate(soldierData.soldierPrefab,targetGrid.transform);

        }
    }
   
}
