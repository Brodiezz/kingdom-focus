using UnityEngine;
using System.Collections;

public class BuildingAnimator : MonoBehaviour
{
    [SerializeField] private Building building;
    [SerializeField] private ParticleSystem constructionParticles;
    [SerializeField] private ParticleSystem upgradeParticles;
    [SerializeField] private Light buildingLight;
    
    private Animator animator;
    private float constructionProgress = 0f;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    public void PlayConstructionAnimation(float duration)
    {
        StartCoroutine(ConstructionSequence(duration));
    }
    
    private IEnumerator ConstructionSequence(float duration)
    {
        constructionProgress = 0f;
        float elapsed = 0f;
        
        // Play construction particles
        if (constructionParticles != null)
        {
            constructionParticles.Play();
        }
        
        // Dim light while constructing
        if (buildingLight != null)
        {
            buildingLight.intensity = 0.5f;
        }
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            constructionProgress = elapsed / duration;
            
            // Visual feedback of construction progress
            UpdateConstructionVisuals(constructionProgress);
            
            yield return null;
        }
        
        constructionProgress = 1f;
        
        // Stop particles and restore light
        if (constructionParticles != null)
        {
            constructionParticles.Stop();
        }
        
        if (buildingLight != null)
        {
            buildingLight.intensity = 1f;
        }
        
        Debug.Log($"✅ {building.GetBuildingName()} construction complete!");
    }
    
    public void PlayUpgradeAnimation()
    {
        StartCoroutine(UpgradeSequence());
    }
    
    private IEnumerator UpgradeSequence()
    {
        // Play upgrade particles
        if (upgradeParticles != null)
        {
            upgradeParticles.Play();
        }
        
        // Pulse light effect
        float pulseDuration = 1f;
        float elapsed = 0f;
        float originalIntensity = buildingLight ? buildingLight.intensity : 1f;
        
        while (elapsed < pulseDuration)
        {
            elapsed += Time.deltaTime;
            float pulse = Mathf.Sin(elapsed * Mathf.PI / pulseDuration) * 0.5f;
            
            if (buildingLight != null)
            {
                buildingLight.intensity = originalIntensity + pulse;
            }
            
            yield return null;
        }
        
        if (buildingLight != null)
        {
            buildingLight.intensity = originalIntensity;
        }
    }
    
    private void UpdateConstructionVisuals(float progress)
    {
        // Scale building from 0 to 1
        Vector3 scale = Vector3.one * progress;
        transform.localScale = scale;
        
        // Fade in material
        Material mat = GetComponent<Renderer>().material;
        Color color = mat.color;
        color.a = progress;
        mat.color = color;
    }
    
    public void PlayIdleAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Idle");
        }
    }
    
    public void PlayDamageAnimation()
    {
        StartCoroutine(DamageFlash());
    }
    
    private IEnumerator DamageFlash()
    {
        Material mat = GetComponent<Renderer>().material;
        Color originalColor = mat.color;
        
        // Flash red
        for (int i = 0; i < 3; i++)
        {
            mat.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            mat.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
