using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

public class ApiService : MonoBehaviour
{
    private static ApiService instance;
    private string apiUrl = "http://localhost:3000/api";
    private string authToken;
    private int userId;
    
    public static ApiService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ApiService>();
                if (instance == null)
                {
                    GameObject go = new GameObject("ApiService");
                    instance = go.AddComponent<ApiService>();
                }
            }
            return instance;
        }
    }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    public void SetAuthToken(string token, int id)
    {
        authToken = token;
        userId = id;
        PlayerPrefs.SetString("AuthToken", token);
        PlayerPrefs.SetInt("UserId", id);
    }
    
    public void LoadAuthFromStorage()
    {
        authToken = PlayerPrefs.GetString("AuthToken", "");
        userId = PlayerPrefs.GetInt("UserId", 0);
    }
    
    public IEnumerator Register(string username, string email, string password, string kingdomName, string heroName, System.Action<ApiResponse> callback)
    {
        var data = new
        {
            username,
            email,
            password,
            kingdomName,
            heroName
        };
        
        yield return Post($"{apiUrl}/auth/register", data, callback);
    }
    
    public IEnumerator Login(string username, string password, System.Action<ApiResponse> callback)
    {
        var data = new { username, password };
        yield return Post($"{apiUrl}/auth/login", data, callback);
    }
    
    public IEnumerator GetKingdom(System.Action<ApiResponse> callback)
    {
        yield return Get($"{apiUrl}/kingdom/{userId}", callback);
    }
    
    public IEnumerator BuildBuilding(string buildingType, int gridX, int gridZ, int width = 4, int height = 4, System.Action<ApiResponse> callback = null)
    {
        var data = new { buildingType, gridX, gridZ, width, height };
        yield return Post($"{apiUrl}/kingdom/{userId}/buildings", data, callback);
    }
    
    public IEnumerator UpgradeBuilding(int buildingId, System.Action<ApiResponse> callback = null)
    {
        yield return Put($"{apiUrl}/kingdom/{userId}/buildings/{buildingId}", new { }, callback);
    }
    
    public IEnumerator CreateQuest(string questName, string description, int durationMinutes, string difficulty = "Medium", System.Action<ApiResponse> callback = null)
    {
        var data = new { questName, description, durationMinutes, difficulty };
        yield return Post($"{apiUrl}/quests", data, callback);
    }
    
    public IEnumerator StartQuest(int questId, System.Action<ApiResponse> callback = null)
    {
        yield return Post($"{apiUrl}/quests/{questId}/start", new { }, callback);
    }
    
    public IEnumerator CompleteQuest(int questId, System.Action<ApiResponse> callback = null)
    {
        yield return Post($"{apiUrl}/quests/{questId}/complete", new { }, callback);
    }
    
    public IEnumerator GetQuests(System.Action<ApiResponse> callback = null)
    {
        yield return Get($"{apiUrl}/quests", callback);
    }
    
    public IEnumerator GetStreak(System.Action<ApiResponse> callback = null)
    {
        yield return Get($"{apiUrl}/quests/{userId}/streak", callback);
    }
    
    public IEnumerator GetLeaderboard(System.Action<ApiResponse> callback = null)
    {
        yield return Get($"{apiUrl}/leaderboards/global", callback);
    }
    
    public IEnumerator GetAnalytics(System.Action<ApiResponse> callback = null)
    {
        yield return Get($"{apiUrl}/analytics/{userId}", callback);
    }
    
    public IEnumerator CreateGuild(string guildName, string description, System.Action<ApiResponse> callback = null)
    {
        var data = new { guildName, description };
        yield return Post($"{apiUrl}/guilds", data, callback);
    }
    
    public IEnumerator GetGuilds(System.Action<ApiResponse> callback = null)
    {
        yield return Get($"{apiUrl}/guilds", callback);
    }
    
    public IEnumerator JoinGuild(int guildId, System.Action<ApiResponse> callback = null)
    {
        yield return Post($"{apiUrl}/guilds/{guildId}/join", new { }, callback);
    }
    
    private IEnumerator Get(string url, System.Action<ApiResponse> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("Authorization", $"Bearer {authToken}");
            www.SetRequestHeader("Content-Type", "application/json");
            
            yield return www.SendWebRequest();
            
            ApiResponse response = new ApiResponse();
            
            if (www.result != UnityWebRequest.Result.Success)
            {
                response.success = false;
                response.error = www.error;
                Debug.LogError($"GET Error: {www.error}");
            }
            else
            {
                response.success = true;
                response.data = www.downloadHandler.text;
            }
            
            callback?.Invoke(response);
        }
    }
    
    private IEnumerator Post(string url, object data, System.Action<ApiResponse> callback)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        
        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            
            www.SetRequestHeader("Authorization", $"Bearer {authToken}");
            www.SetRequestHeader("Content-Type", "application/json");
            
            yield return www.SendWebRequest();
            
            ApiResponse response = new ApiResponse();
            
            if (www.result != UnityWebRequest.Result.Success)
            {
                response.success = false;
                response.error = www.error;
                Debug.LogError($"POST Error: {www.error}");
            }
            else
            {
                response.success = true;
                response.data = www.downloadHandler.text;
            }
            
            callback?.Invoke(response);
        }
    }
    
    private IEnumerator Put(string url, object data, System.Action<ApiResponse> callback)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        
        using (UnityWebRequest www = new UnityWebRequest(url, "PUT"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            
            www.SetRequestHeader("Authorization", $"Bearer {authToken}");
            www.SetRequestHeader("Content-Type", "application/json");
            
            yield return www.SendWebRequest();
            
            ApiResponse response = new ApiResponse();
            
            if (www.result != UnityWebRequest.Result.Success)
            {
                response.success = false;
                response.error = www.error;
                Debug.LogError($"PUT Error: {www.error}");
            }
            else
            {
                response.success = true;
                response.data = www.downloadHandler.text;
            }
            
            callback?.Invoke(response);
        }
    }
}

public class ApiResponse
{
    public bool success;
    public string data;
    public string error;
}
