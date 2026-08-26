using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardPanel : MonoBehaviour
{
    [SerializeField] private Transform leaderboardContent;
    [SerializeField] private GameObject leaderboardEntryPrefab;
    [SerializeField] private UnityEngine.UI.Button refreshButton;
    
    private void Start()
    {
        refreshButton.onClick.AddListener(LoadLeaderboard);
        LoadLeaderboard();
    }
    
    private void LoadLeaderboard()
    {
        StartCoroutine(ApiService.Instance.GetLeaderboard(response => {
            if (response.success)
            {
                DisplayLeaderboard(response.data);
            }
        }));
    }
    
    private void DisplayLeaderboard(string jsonData)
    {
        foreach (Transform child in leaderboardContent)
        {
            Destroy(child.gameObject);
        }
        
        var leaderboardJson = JsonUtility.FromJson<LeaderboardResponse>(jsonData);
        
        int rank = 1;
        foreach (var entry in leaderboardJson.leaderboard)
        {
            GameObject entryObj = Instantiate(leaderboardEntryPrefab, leaderboardContent);
            TextMeshProUGUI entryText = entryObj.GetComponentInChildren<TextMeshProUGUI>();
            entryText.text = $"#{rank} {entry.username} - Level {entry.level} | {entry.focus_time}min";
            rank++;
        }
    }
}

[System.Serializable]
public class LeaderboardResponse
{
    public LeaderboardEntry[] leaderboard;
}

[System.Serializable]
public class LeaderboardEntry
{
    public int id;
    public string username;
    public int level;
    public int focus_time;
    public int rank;
}
