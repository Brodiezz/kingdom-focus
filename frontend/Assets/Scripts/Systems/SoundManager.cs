using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip uiClickSound;
    [SerializeField] private AudioClip questCompleteSound;
    [SerializeField] private AudioClip levelUpSound;
    [SerializeField] private AudioClip buildingCompleteSound;
    [SerializeField] private AudioClip goldCollectSound;
    [SerializeField] private float masterVolume = 1f;
    
    private static SoundManager instance;
    private AudioSource audioSource;
    
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }
            return instance;
        }
    }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    public void PlayUIClick()
    {
        PlaySound(uiClickSound, 0.5f);
    }
    
    public void PlayQuestComplete()
    {
        PlaySound(questCompleteSound, 0.8f);
    }
    
    public void PlayLevelUp()
    {
        PlaySound(levelUpSound, 0.9f);
    }
    
    public void PlayBuildingComplete()
    {
        PlaySound(buildingCompleteSound, 0.7f);
    }
    
    public void PlayGoldCollect()
    {
        PlaySound(goldCollectSound, 0.6f);
    }
    
    private void PlaySound(AudioClip clip, float volume)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, volume * masterVolume);
        }
    }
    
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
    }
}
