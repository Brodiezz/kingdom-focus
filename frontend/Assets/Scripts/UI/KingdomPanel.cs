using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class KingdomPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI kingdomNameText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI goldPerHourText;
    [SerializeField] private Transform buildingGridParent;
    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private UnityEngine.UI.Button[] buildingButtons;
    
    private int currentKingdomId;
    private Dictionary<string, GameObject> buildingInstances = new Dictionary<string, GameObject>();
    
    private void Start()
    {
        LoadKingdom();
        
        for (int i = 0; i < buildingButtons.Length; i++)
        {
            int index = i;
            buildingButtons[i].onClick.AddListener(() => OnBuildingButtonClick(index));
        }
    }
    
    private void LoadKingdom()
    {
        StartCoroutine(ApiService.Instance.GetKingdom(response => {
            if (response.success)
            {
                var kingdomData = JsonUtility.FromJson<KingdomData>(response.data);
                UpdateKingdomUI(kingdomData);
            }
        }));
    }
    
    private void UpdateKingdomUI(KingdomData data)
    {
        kingdomNameText.text = data.kingdom.name;
        goldText.text = $"💰 {data.kingdom.gold}";
        levelText.text = $"Level {data.kingdom.level}";
        goldPerHourText.text = $"{data.totalGoldPerHour}/hr";
        
        currentKingdomId = data.kingdom.id;
        
        foreach (var building in data.buildings)
        {
            DisplayBuilding(building);
        }
    }
    
    private void DisplayBuilding(BuildingData building)
    {
        GameObject buildingObj = Instantiate(buildingPrefab, buildingGridParent);
        buildingObj.name = $"{building.building_type}_{building.id}";
        
        Vector3 pos = new Vector3(building.grid_x * 2, 0, building.grid_z * 2);
        buildingObj.transform.localPosition = pos;
        
        TextMeshProUGUI buildingText = buildingObj.GetComponentInChildren<TextMeshProUGUI>();
        buildingText.text = $"{building.building_type}\nLv.{building.level}";
        
        buildingInstances[building.id.ToString()] = buildingObj;
    }
    
    private void OnBuildingButtonClick(int buttonIndex)
    {
        string[] buildingTypes = { "Barracks", "Woodcutter", "Farm", "Tower", "Forge", "Market", "Castle", "Arcane", "Guild" };
        string buildingType = buildingTypes[buttonIndex];
        
        int gridX = Random.Range(0, 10);
        int gridZ = Random.Range(0, 10);
        
        StartCoroutine(ApiService.Instance.BuildBuilding(buildingType, gridX, gridZ, 4, 4, response => {
            if (response.success)
            {
                Debug.Log($"Building {buildingType} construction started!");
                LoadKingdom();
            }
            else
            {
                Debug.LogError($"Failed to build: {response.error}");
            }
        }));
    }
}

[System.Serializable]
public class KingdomData
{
    public Kingdom kingdom;
    public BuildingData[] buildings;
    public int totalGoldPerHour;
}

[System.Serializable]
public class Kingdom
{
    public int id;
    public int user_id;
    public string name;
    public int level;
    public int gold;
    public int gold_per_hour;
}

[System.Serializable]
public class BuildingData
{
    public int id;
    public int kingdom_id;
    public string building_type;
    public int level;
    public int grid_x;
    public int grid_z;
    public int width;
    public int height;
    public int gold_per_hour;
    public int upgrade_cost;
}
