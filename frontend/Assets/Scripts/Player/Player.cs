using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private string playerName;
    [SerializeField] private int level = 1;
    [SerializeField] private int experience = 0;
    [SerializeField] private int gold = 1000;
    [SerializeField] private int currentStreak = 0;
    [SerializeField] private int longestStreak = 0;
    
    private int experienceToNextLevel = 1000;
    
    public void AddXP(int amount)
    {
        experience += amount;
        Debug.Log($"⭐ Gained {amount} XP! Total: {experience}");
        
        if (experience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }
    
    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log($"💰 Gained {amount} Gold! Total: {gold}");
    }
    
    public void SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            Debug.Log($"💸 Spent {amount} Gold. Remaining: {gold}");
            return;
        }
        Debug.LogWarning("Insufficient gold!");
    }
    
    private void LevelUp()
    {
        level++;
        experience = 0;
        experienceToNextLevel = (int)(experienceToNextLevel * 1.1f);
        Debug.Log($"🎉 LEVEL UP! You are now level {level}");
    }
    
    public void IncrementStreak()
    {
        currentStreak++;
        if (currentStreak > longestStreak)
        {
            longestStreak = currentStreak;
        }
        Debug.Log($"🔥 Streak: {currentStreak} days!");
    }
    
    public void ResetStreak()
    {
        currentStreak = 0;
    }
    
    // Getters
    public string GetPlayerName() => playerName;
    public int GetLevel() => level;
    public int GetExperience() => experience;
    public int GetGold() => gold;
    public int GetCurrentStreak() => currentStreak;
    public int GetLongestStreak() => longestStreak;
}
