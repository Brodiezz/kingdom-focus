using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private Player player;
    [SerializeField] private Kingdom kingdom;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private UIManager uiManager;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    private void Start()
    {
        // Initialize game
        InitializeGame();
    }
    
    public void InitializeGame()
    {
        Debug.Log("🏰 Kingdom Focus: Initializing game...");
        
        // Load player data
        // Load kingdom
        // Load quests
        // Initialize UI
    }
    
    public void StartQuest(Quest quest)
    {
        questManager.StartQuest(quest);
    }
    
    public void CompleteQuest(Quest quest)
    {
        // Award gold and XP
        player.AddGold(quest.goldReward);
        player.AddXP(quest.xpReward);
        
        // Update streak
        player.IncrementStreak();
        
        // Update kingdom
        kingdom.UpdateResources();
        
        questManager.CompleteQuest(quest);
    }
    
    public Player GetPlayer() => player;
    public Kingdom GetKingdom() => kingdom;
}
