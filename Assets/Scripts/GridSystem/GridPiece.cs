using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GridPiece : MonoBehaviour
{
    public bool IsAvailable { get; private set; }
    public Vector2Int GridPosition { get; private set; }
    public List<GridPiece> Neighbours { get;  set; }
    public GameObject CurrentObjectOnGrid { get; private set; }

    [HideInInspector] public int GScore;
    [HideInInspector] public int HScore;

    private GridManager gridManager;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        Neighbours = new List<GridPiece>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    

    public void SetGridData(Vector2Int gridPosition, int gScore, int hScore)
    {
        GridPosition = gridPosition;
        GScore = gScore;
        HScore = hScore;
        IsAvailable = true;
    }

    public GridPiece GetNeighbour(GridDirections direction)
    {
        return direction switch
        {
            GridDirections.Left => Neighbours.Find(neighbour => neighbour.GridPosition.x == GridPosition.x - 1),
            GridDirections.Right => Neighbours.Find(neighbour => neighbour.GridPosition.x == GridPosition.x + 1),
            GridDirections.Up => Neighbours.Find(neighbour => neighbour.GridPosition.y == GridPosition.y - 1),
            GridDirections.Down => Neighbours.Find(neighbour => neighbour.GridPosition.y == GridPosition.y + 1),
            _ => null
        };
    }

    public void SetObjectOnGrid(GameObject obj, Vector2Int size)
    {
       if( IsPossiblePlaceObject(size))
        {
            PlaceObjectOnGrid(obj, size);

            OccupySpace(size);

            IsAvailable = false;

            ApplyBlackColor();
        }
        else
        {
            Debug.LogError("Not enough space to place the object.");
        }
    }

    public bool IsPossiblePlaceObject(Vector2Int size)
    {
        if (IsSpaceAvailable(size))
        {
            return true;
        }
        else
        {
            return false;

        }
    }
    private void PlaceObjectOnGrid(GameObject obj, Vector2Int size)
    {
        Vector3Int offset = new Vector3Int(size.x / 2, size.y / 2, 0);

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector2Int targetPos = GridPosition + new Vector2Int(x - offset.x, y - offset.y);
                GridPiece targetPiece = gridManager.GetGridPieceAtPosition(targetPos);
                if (targetPiece != null)
                {
                    targetPiece.CurrentObjectOnGrid = obj;
                    obj.transform.parent = targetPiece.transform;
                    obj.transform.localPosition=new Vector3(.3f,0,0);
                }
            }
        }
    }
    private bool IsSpaceAvailable(Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector2Int targetPos = GridPosition + new Vector2Int(x, y);
                if (!gridManager.IsGridPieceAvailable(targetPos))
                    return false;
            }
        }
        return true;
    }

    private void OccupySpace(Vector2Int size)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector2Int targetPos = GridPosition + new Vector2Int(x, y);
                GridPiece targetPiece = gridManager.GetGridPieceAtPosition(targetPos);
                if (targetPiece != null)
                {
                    targetPiece.IsAvailable = false;
                    targetPiece.ApplyBlackColor();
                    targetPiece.CurrentObjectOnGrid = CurrentObjectOnGrid;
                }
            }
        }
    }

    public void ApplyBlackColor()
    {
        spriteRenderer.color = Color.black;
    }
    public void ApplyWhiteColor()
    {
        spriteRenderer.color = Color.white;
    }
}