using UnityEngine;

public class IsometricRenderer : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float orthographicSize = 10f;
    [SerializeField] private bool useOrthographic = true;
    
    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        
        SetupIsometricView();
    }
    
    public void SetupIsometricView()
    {
        // Set camera to orthographic for clean 2.5D look
        mainCamera.orthographic = useOrthographic;
        mainCamera.orthographicSize = orthographicSize;
        
        // Position camera for isometric view (45 degree angle)
        transform.rotation = Quaternion.Euler(30, 45, 0);
        
        // Set up sorting layers for depth
        SetupSortingLayers();
        
        Debug.Log("📷 Isometric camera configured");
    }
    
    private void SetupSortingLayers()
    {
        // Layers should be sorted by Y position for proper depth
        // This will be handled by the Y-sort in the SpriteRenderer
        // or through custom sorting in mesh renderers
    }
    
    public void SetOrthographicSize(float newSize)
    {
        orthographicSize = newSize;
        mainCamera.orthographicSize = orthographicSize;
    }
    
    public Vector3 WorldToScreenIsometric(Vector3 worldPosition)
    {
        return mainCamera.WorldToScreenPoint(worldPosition);
    }
    
    public Vector3 ScreenToWorldIsometric(Vector3 screenPosition)
    {
        screenPosition.z = 10f; // Distance from camera
        return mainCamera.ScreenToWorldPoint(screenPosition);
    }
}
