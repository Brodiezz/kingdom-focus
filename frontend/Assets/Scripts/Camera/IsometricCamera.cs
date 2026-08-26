using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 20f;
    [SerializeField] private float height = 15f;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float rotationX = 30f;
    [SerializeField] private float rotationY = 45f;
    
    private Vector3 offset;
    private Camera mainCamera;
    
    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        CalculateOffset();
    }
    
    private void LateUpdate()
    {
        if (target != null)
        {
            FollowTarget();
        }
    }
    
    private void CalculateOffset()
    {
        // Calculate isometric offset based on rotations
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        offset = rotation * new Vector3(0, height, distance);
    }
    
    private void FollowTarget()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        transform.LookAt(target.position + Vector3.up * (height * 0.3f));
    }
    
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    
    public void ZoomIn(float amount)
    {
        distance = Mathf.Max(5f, distance - amount);
        CalculateOffset();
    }
    
    public void ZoomOut(float amount)
    {
        distance = Mathf.Min(50f, distance + amount);
        CalculateOffset();
    }
    
    public void RotateView(float horizontal, float vertical)
    {
        rotationY += horizontal;
        rotationX = Mathf.Clamp(rotationX + vertical, 15f, 60f);
        CalculateOffset();
    }
}
