using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class GridGenerator : MonoBehaviour
{
   [Range(1, 200)] public int width = 10;
    [Range(1, 200)] public int height = 10;
    public GameObject gridPiecePrefab;
    [Range(0.1f, 2f)] public float scale = 1f;
    [Range(0.1f, 2f)] public float xOffset = 1f;
    [Range(0.1f, 2f)] public float yOffset = 1f;

    [Header("For Testing Grid")]
    public GridPiece startGrid;
    public GridPiece endGrid;
    public GridManager generatedGridManager;

#if UNITY_EDITOR
    public void GenerateGrid()
    {
        if (!ValidateInputs()) return;

        GameObject gridParent = CreateGridParent();
        InitializeGrid(gridParent);
        SetGridManager(gridParent);
    }

    private bool ValidateInputs()
    {
        if (gridPiecePrefab == null)
        {
            Debug.LogError("GridPiecePrefab is not assigned.");
            return false;
        }
        if (width <= 0 || height <= 0)
        {
            Debug.LogError("Grid dimensions must be greater than zero.");
            return false;
        }
        return true;
    }

    private GameObject CreateGridParent()
    {
        GameObject gridParent = new GameObject("Grid Parent");
        gridParent.AddComponent<GridManager>();
        return gridParent;
    }

    private void InitializeGrid(GameObject gridParent)
    {
        GridManager gridManager = gridParent.GetComponent<GridManager>();
        gridManager.grid = new GridPiece[width, height];

        float startX = -(width - 1) * xOffset;
        float startY = (height - 1) * yOffset;

        for (int i = 0; i < height; i++)
        {
            float currentX = startX;
            for (int j = 0; j < width; j++)
            {
                GameObject tempGrid = CreateGridPiece(gridParent, currentX, startY);
                gridManager.grid[j, i] = tempGrid.GetComponent<GridPiece>();
                gridManager.grid[j, i].SetGridData(new Vector2Int(j, i), 0, 0);
                currentX += xOffset * 2;
            }
            startY -= yOffset * 2;
        }
    }

    private GameObject CreateGridPiece(GameObject parent, float posX, float posY)
    {
        GameObject gridPiece = PrefabUtility.InstantiatePrefab(gridPiecePrefab, parent.transform) as GameObject;
        gridPiece.transform.localScale = Vector2.one * scale;
        gridPiece.transform.localPosition = new Vector2(posX, posY);
        return gridPiece;
    }

    private void SetGridManager(GameObject gridParent)
    {
        generatedGridManager = gridParent.GetComponent<GridManager>();
        generatedGridManager.width = width;
        generatedGridManager.height = height;
        generatedGridManager.SetNeighbors();
    }
    
#endif
}

