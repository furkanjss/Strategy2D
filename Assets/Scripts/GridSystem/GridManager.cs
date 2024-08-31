using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] public GridPiece[,] grid;
    [SerializeField] public int width;
    [SerializeField] public int height;

    private void Start()
    {
        InitializeGrid();
        SetNeighbors();
    }

    private void InitializeGrid()
    {
        grid = new GridPiece[width, height];
        int index = 0;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                grid[j, i] = transform.GetChild(index).GetComponent<GridPiece>();
                grid[j, i].SetGridData(new Vector2Int(j, i), int.MaxValue, 0);
                index++;
            }
        }
    }

    public bool IsGridPieceAvailable(Vector2Int position)
    {
        return IsValidGridPosition(position) && grid[position.x, position.y].IsAvailable;
    }

    public GridPiece GetGridPieceAtPosition(Vector2Int position)
    {
        if (IsValidGridPosition(position))
            return grid[position.x, position.y];
        return null;
    }

    public List<GridPiece> FindShortestPath(GridPiece start, GridPiece end)
    {
        if (start == null || end == null || !IsValidGridPosition(start.GridPosition) || !IsValidGridPosition(end.GridPosition))
        {
            Debug.LogError("Invalid start or end grid position!");
            return null;
        }

        var openSet = new List<GridPiece> { start };
        var closedSet = new HashSet<GridPiece>();
        var cameFrom = new Dictionary<GridPiece, GridPiece>();

        while (openSet.Count > 0)
        {
            var current = GetLowestFscore(openSet);
            if (current == end)
            {
                return ReconstructPath(cameFrom, current);
            }

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (var neighbor in current.Neighbours)
            {
                if (closedSet.Contains(neighbor) || !neighbor.IsAvailable) continue;

                var tentativeGScore = current.GScore + 1;

                if (!openSet.Contains(neighbor) || tentativeGScore < neighbor.GScore)
                {
                    cameFrom[neighbor] = current;
                    neighbor.GScore = tentativeGScore;
                    neighbor.HScore = Heuristic(neighbor, end);
                    openSet.Add(neighbor);
                }
            }
        }

        Debug.Log("No path found!");
        return null;
    }

    private bool IsValidGridPosition(Vector2Int position)
    {
        return position.x >= 0 && position.x < grid.GetLength(0) &&
               position.y >= 0 && position.y < grid.GetLength(1);
    }

    private GridPiece GetLowestFscore(List<GridPiece> openSet)
    {
        GridPiece lowestFscore = null;
        float lowestF = float.MaxValue;

        foreach (var node in openSet)
        {
            float f = node.GScore + node.HScore;
            if (f < lowestF)
            {
                lowestF = f;
                lowestFscore = node;
            }
        }

        return lowestFscore;
    }

    private List<GridPiece> ReconstructPath(Dictionary<GridPiece, GridPiece> cameFrom, GridPiece current)
    {
        var path = new List<GridPiece> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }
        path.Reverse();
        return path;
    }

    private int Heuristic(GridPiece a, GridPiece b)
    {
        return Mathf.Abs(a.GridPosition.x - b.GridPosition.x) + Mathf.Abs(a.GridPosition.y - b.GridPosition.y);
    }

    private List<GridPiece> GetNeighbors(GridPiece node)
    {
        var neighbors = new List<GridPiece>();
        var pos = node.GridPosition;

        if (IsValidGridPosition(pos + Vector2Int.right)) neighbors.Add(grid[pos.x + 1, pos.y]);
        if (IsValidGridPosition(pos + Vector2Int.left)) neighbors.Add(grid[pos.x - 1, pos.y]);
        if (IsValidGridPosition(pos + Vector2Int.up)) neighbors.Add(grid[pos.x, pos.y + 1]);
        if (IsValidGridPosition(pos + Vector2Int.down)) neighbors.Add(grid[pos.x, pos.y - 1]);

        return neighbors;
    }

    public void SetNeighbors()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                grid[i, j].Neighbours = GetNeighbors(grid[i, j]);
            }
        }
    }
}
