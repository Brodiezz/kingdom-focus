using UnityEngine;
using TMPro;
using System.Collections;

public class QuestPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField questNameInput;
    [SerializeField] private TMP_InputField descriptionInput;
    [SerializeField] private TMP_InputField durationInput;
    [SerializeField] private TMP_Dropdown difficultyDropdown;
    [SerializeField] private Button scheduleButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Button completeButton;
    [SerializeField] private TextMeshProUGUI questStatusText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI rewardsText;
    
    private Quest activeQuest;
    private float questStartTime;
    private float questDuration;
    private bool isQuestActive = false;
    
    private void Start()
    {
        scheduleButton.onClick.AddListener(OnScheduleQuest);
        startButton.onClick.AddListener(OnStartQuest);
        completeButton.onClick.AddListener(OnCompleteQuest);
    }
    
    private void Update()
    {
        if (isQuestActive)
        {
            UpdateQuestTimer();
        }
    }
    
    private void OnScheduleQuest()
    {
        string questName = questNameInput.text;
        string description = descriptionInput.text;
        int duration = int.Parse(durationInput.text);
        string difficulty = difficultyDropdown.options[difficultyDropdown.value].text;
        
        if (string.IsNullOrEmpty(questName) || duration <= 0)
        {
            Debug.LogError("Invalid quest data");
            return;
        }
        
        StartCoroutine(ApiService.Instance.CreateQuest(questName, description, duration, difficulty, response => {
            if (response.success)
            {
                questStatusText.text = "Quest Scheduled! Ready to begin?";
                startButton.interactable = true;
            }
        }));
    }
    
    private void OnStartQuest()
    {
        // Get latest quest and start it
        StartCoroutine(ApiService.Instance.GetQuests(response => {
            if (response.success)
            {
                // Parse quests and start the latest
                questStatusText.text = "🎯 QUEST STARTED - FOCUS NOW!";
                isQuestActive = true;
                questStartTime = Time.time;
                questDuration = int.Parse(durationInput.text) * 60; // Convert to seconds
                startButton.interactable = false;
                completeButton.interactable = true;
            }
        }));
    }
    
    private void OnCompleteQuest()
    {
        isQuestActive = false;
        
        // Get latest quest ID and complete it
        StartCoroutine(ApiService.Instance.GetQuests(response => {
            if (response.success)
            {
                questStatusText.text = "✅ QUEST COMPLETED!";
                questStatusText.color = Color.green;
                
                // Show rewards
                rewardsText.text = "💰 +100 Gold\n⭐ +250 XP\n🔥 Streak +1";
                
                completeButton.interactable = false;
                scheduleButton.interactable = true;
                
                // Refresh UI
                StartCoroutine(GameManager.Instance.CompleteQuest(null));
            }
        }));
    }
    
    private void UpdateQuestTimer()
    {
        float elapsed = Time.time - questStartTime;
        float remaining = questDuration - elapsed;
        
        if (remaining <= 0)
        {
            timerText.text = "⏰ TIME'S UP!";
            timerText.color = Color.red;
        }
        else
        {
            int minutes = (int)remaining / 60;
            int seconds = (int)remaining % 60;
            timerText.text = $"⏱️ {minutes:D2}:{seconds:D2}";
        }
    }
}
