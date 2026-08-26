using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI streakText;
    [SerializeField] private TextMeshProUGUI questTimerText;
    
    private Player player;
    private Quest activeQuest;
    
    private void Start()
    {
        player = GameManager.Instance.GetPlayer();
    }
    
    private void Update()
    {
        UpdateDisplay();
    }
    
    private void UpdateDisplay()
    {
        goldText.text = $"💰 {player.GetGold()}";
        xpText.text = $"⭐ {player.GetExperience()}";
        levelText.text = $"Level {player.GetLevel()}";
        streakText.text = $"🔥 {player.GetCurrentStreak()} day streak";
        
        // Update quest timer
        activeQuest = GameManager.Instance.GetKingdom().GetBuildings().Count > 0 
            ? null 
            : activeQuest; // Placeholder
    }
}
