using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Quest> availableQuests = new List<Quest>();
    [SerializeField] private Quest currentActiveQuest;
    
    public void StartQuest(Quest quest)
    {
        if (currentActiveQuest != null)
        {
            Debug.LogWarning("Quest already in progress!");
            return;
        }
        
        currentActiveQuest = quest;
        currentActiveQuest.StartQuest();
    }
    
    public void CompleteQuest(Quest quest)
    {
        if (currentActiveQuest != quest)
        {
            Debug.LogWarning("Quest is not active!");
            return;
        }
        
        currentActiveQuest.CompleteQuest();
        currentActiveQuest = null;
        
        // Reward player through GameManager
        GameManager.Instance.GetPlayer().AddGold(quest.goldReward);
        GameManager.Instance.GetPlayer().AddXP(quest.xpReward);
    }
    
    public void AddQuest(Quest quest)
    {
        availableQuests.Add(quest);
    }
    
    public Quest GetCurrentQuest() => currentActiveQuest;
    public List<Quest> GetAvailableQuests() => availableQuests;
}
