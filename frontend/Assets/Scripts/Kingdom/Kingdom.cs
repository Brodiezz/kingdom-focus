using UnityEngine;
using System.Collections.Generic;

public class Kingdom : MonoBehaviour
{
    [SerializeField] private string kingdomName;
    [SerializeField] private int level = 1;
    [SerializeField] private List<Building> buildings = new List<Building>();
    
    private int goldPerHour = 10;
    private float lastResourceUpdateTime;
    
    private void Start()
    {
        lastResourceUpdateTime = Time.time;
    }
    
    private void Update()
    {
        // Generate passive resources
        if (Time.time - lastResourceUpdateTime >= 3600) // 1 hour
        {
            UpdateResources();
            lastResourceUpdateTime = Time.time;
        }
    }
    
    public void UpdateResources()
    {
        // Calculate resources from buildings
        int totalGoldPerHour = goldPerHour;
        
        foreach (Building building in buildings)
        {
            totalGoldPerHour += building.GetGoldPerHour();
        }
        
        Debug.Log($"🏰 Kingdom generating {totalGoldPerHour} gold/hour");
    }
    
    public void AddBuilding(Building building)
    {
        buildings.Add(building);
        Debug.Log($"🏗️ Built {building.GetBuildingName()}!");
    }
    
    public void UpgradeBuilding(Building building)
    {
        building.Upgrade();
        Debug.Log($"⬆️ Upgraded {building.GetBuildingName()} to level {building.GetLevel()}!");
    }
    
    // Getters
    public string GetKingdomName() => kingdomName;
    public int GetLevel() => level;
    public List<Building> GetBuildings() => buildings;
}
