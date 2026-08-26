using UnityEngine;
using System.Collections.Generic;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private int gridWidth = 20;
    [SerializeField] private int gridHeight = 20;
    [SerializeField] private float cellSize = 2f;
    [SerializeField] private Material gridMaterial;
    
    private GridCell[,] grid;
    private List<Building> placedBuildings = new List<Building>();
    
    private void Start()
    {
        InitializeGrid();
        DrawGrid();
    }
    
    private void InitializeGrid()
    {
        grid = new GridCell[gridWidth, gridHeight];
        
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                grid[x, z] = new GridCell(x, z);
            }
        }
        
        Debug.Log($"🏰 Grid initialized: {gridWidth}x{gridHeight}");
    }
    
    private void DrawGrid()
    {
        // Create visual grid representation (optional)
        for (int x = 0; x <= gridWidth; x++)
        {
            Debug.DrawLine(
                new Vector3(x * cellSize, 0, 0),
                new Vector3(x * cellSize, 0, gridHeight * cellSize),
                Color.white,
                100f
            );
        }
        
        for (int z = 0; z <= gridHeight; z++)
        {
            Debug.DrawLine(
                new Vector3(0, 0, z * cellSize),
                new Vector3(gridWidth * cellSize, 0, z * cellSize),
                Color.white,
                100f
            );
        }
    }
    
    public bool CanPlaceBuilding(int gridX, int gridZ, int width, int height)
    {
        if (gridX + width > gridWidth || gridZ + height > gridHeight)
        {
            return false;
        }
        
        for (int x = gridX; x < gridX + width; x++)
        {
            for (int z = gridZ; z < gridZ + height; z++)
            {
                if (grid[x, z].IsOccupied)
                {
                    return false;
                }
            }
        }
        
        return true;
    }
    
    public void PlaceBuilding(Building building, int gridX, int gridZ, int width, int height)
    {
        if (!CanPlaceBuilding(gridX, gridZ, width, height))
        {
            Debug.LogWarning("Cannot place building: space occupied or out of bounds");
            return;
        }
        
        // Mark cells as occupied
        for (int x = gridX; x < gridX + width; x++)
        {
            for (int z = gridZ; z < gridZ + height; z++)
            {
                grid[x, z].IsOccupied = true;
                grid[x, z].OccupyingBuilding = building;
            }
        }
        
        // Position building at grid location
        Vector3 worldPos = new Vector3(gridX * cellSize, 0, gridZ * cellSize);
        building.transform.position = worldPos;
        
        placedBuildings.Add(building);
        Debug.Log($"✅ Building placed at grid ({gridX}, {gridZ})");
    }
    
    public Vector3 GridToWorldPosition(int gridX, int gridZ)
    {
        return new Vector3(gridX * cellSize, 0, gridZ * cellSize);
    }
    
    public void GetGridPosition(Vector3 worldPosition, out int gridX, out int gridZ)
    {
        gridX = Mathf.RoundToInt(worldPosition.x / cellSize);
        gridZ = Mathf.RoundToInt(worldPosition.z / cellSize);
    }
    
    public class GridCell
    {
        public int GridX { get; set; }
        public int GridZ { get; set; }
        public bool IsOccupied { get; set; }
        public Building OccupyingBuilding { get; set; }
        
        public GridCell(int x, int z)
        {
            GridX = x;
            GridZ = z;
            IsOccupied = false;
            OccupyingBuilding = null;
        }
    }
}
