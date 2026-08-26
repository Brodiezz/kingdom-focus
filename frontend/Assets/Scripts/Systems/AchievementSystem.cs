using UnityEngine;
using TMPro;
using System.Collections;

public class AchievementSystem : MonoBehaviour
{
    [SerializeField] private GameObject achievementNotificationPrefab;
    [SerializeField] private Transform notificationContainer;
    
    private static AchievementSystem instance;
    
    public static AchievementSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AchievementSystem>();
            }
            return instance;
        }
    }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    public void UnlockAchievement(string badgeName, string description)
    {
        ShowNotification(badgeName, description);
        Debug.Log($"🏆 Achievement Unlocked: {badgeName}");
    }
    
    private void ShowNotification(string title, string description)
    {
        GameObject notification = Instantiate(achievementNotificationPrefab, notificationContainer);
        TextMeshProUGUI titleText = notification.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI descText = notification.transform.Find("Description").GetComponent<TextMeshProUGUI>();
        
        titleText.text = title;
        descText.text = description;
        
        StartCoroutine(FadeOutNotification(notification));
    }
    
    private IEnumerator FadeOutNotification(GameObject notification)
    {
        yield return new WaitForSeconds(3f);
        
        CanvasGroup canvasGroup = notification.GetComponent<CanvasGroup>();
        float elapsed = 0f;
        float fadeDuration = 1f;
        
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            yield return null;
        }
        
        Destroy(notification);
    }
}
