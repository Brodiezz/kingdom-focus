using UnityEngine;
using System;

public class Quest : MonoBehaviour
{
    [SerializeField] public string questName;
    [SerializeField] public string description;
    [SerializeField] public int durationMinutes = 30;
    [SerializeField] public int goldReward = 100;
    [SerializeField] public int xpReward = 250;
    [SerializeField] public Difficulty difficulty;
    
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard,
        Legendary
    }
    
    private DateTime startTime;
    private bool isActive = false;
    
    public void StartQuest()
    {
        isActive = true;
        startTime = DateTime.Now;
        Debug.Log($"⚔️ Quest started: {questName} - Focus for {durationMinutes} minutes!");
    }
    
    public void CompleteQuest()
    {
        isActive = false;
        Debug.Log($"✅ Quest completed: {questName}! Earned {goldReward} gold and {xpReward} XP");
    }
    
    public bool IsTimeRemaining()
    {
        if (!isActive) return false;
        
        TimeSpan elapsed = DateTime.Now - startTime;
        return elapsed.TotalMinutes < durationMinutes;
    }
    
    public int GetTimeRemainingSeconds()
    {
        if (!isActive) return 0;
        
        TimeSpan elapsed = DateTime.Now - startTime;
        int remaining = (durationMinutes * 60) - (int)elapsed.TotalSeconds;
        return Mathf.Max(0, remaining);
    }
}
