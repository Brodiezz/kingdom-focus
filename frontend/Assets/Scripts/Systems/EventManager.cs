using UnityEngine;
using System;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    private static EventManager instance;
    
    public static event Action<int, int> OnQuestCompleted; // questId, gold
    public static event Action<int> OnLevelUp; // newLevel
    public static event Action<int> OnBuildingConstructed; // buildingId
    public static event Action OnStreakMaintained;
    public static event Action OnStreakLost;
    
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EventManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("EventManager");
                    instance = go.AddComponent<EventManager>();
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
    
    public static void TriggerQuestCompleted(int questId, int goldReward)
    {
        OnQuestCompleted?.Invoke(questId, goldReward);
    }
    
    public static void TriggerLevelUp(int newLevel)
    {
        OnLevelUp?.Invoke(newLevel);
    }
    
    public static void TriggerBuildingConstructed(int buildingId)
    {
        OnBuildingConstructed?.Invoke(buildingId);
    }
    
    public static void TriggerStreakMaintained()
    {
        OnStreakMaintained?.Invoke();
    }
    
    public static void TriggerStreakLost()
    {
        OnStreakLost?.Invoke();
    }
}
