using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private string buildingName;
    [SerializeField] private int level = 1;
    [SerializeField] private int goldPerHour = 10;
    [SerializeField] private int buildCost = 500;
    [SerializeField] private int upgradeCost = 250;
    [SerializeField] private float buildTime = 60f; // seconds
    
    private BuildingType type;
    private bool isConstructing = false;
    
    public enum BuildingType
    {
        Barracks,
        Woodcutter,
        Farm,
        Tower,
        Forge,
        Market,
        CastleThrone,
        ArcanneTower,
        GuildHall
    }
    
    public void Construct()
    {
        if (!isConstructing)
        {
            isConstructing = true;
            StartCoroutine(ConstructionTimer());
        }
    }
    
    private System.Collections.IEnumerator ConstructionTimer()
    {
        yield return new WaitForSeconds(buildTime);
        isConstructing = false;
        Debug.Log($"✅ {buildingName} construction complete!");
    }
    
    public void Upgrade()
    {
        level++;
        goldPerHour = (int)(goldPerHour * 1.2f);
        upgradeCost = (int)(upgradeCost * 1.25f);
        Debug.Log($"⬆️ {buildingName} upgraded to level {level}");
    }
    
    // Getters
    public string GetBuildingName() => buildingName;
    public int GetLevel() => level;
    public int GetGoldPerHour() => goldPerHour;
    public int GetBuildCost() => buildCost;
    public int GetUpgradeCost() => upgradeCost;
    public bool IsConstructing() => isConstructing;
}
