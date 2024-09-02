using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitsButton : MonoBehaviour
{
    private SoldierData soldierData;
    private Button unitButton;
    private GridManager _gridManager;
    void Start()
    {
        unitButton = GetComponent<Button>();
        unitButton.onClick.AddListener(CreateUnit);
        _gridManager = FindObjectOfType<GridManager>();
    }

    public void GetSoldierData(SoldierData soldierData)
    {
        this.soldierData = soldierData;
    }
    void CreateUnit()
    {
        GridPiece targetGrid = _gridManager.FindEmptyGrid();
        if (targetGrid!=null)
        {
              GameObject tempSoldier=    Instantiate(soldierData.soldierPrefab,targetGrid.transform);
            targetGrid.SetSoldierOnGrid(tempSoldier);
            tempSoldier.transform.localPosition = Vector3.zero;

        }
    }
   
}
