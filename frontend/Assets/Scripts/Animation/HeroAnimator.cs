using UnityEngine;

public class HeroAnimator : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    
    private Vector3 moveDirection;
    private bool isMoving = false;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        HandleInput();
        UpdateAnimations();
    }
    
    private void HandleInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        moveDirection = new Vector3(horizontal, 0, vertical).normalized;
        isMoving = moveDirection.magnitude > 0.1f;
    }
    
    private void UpdateAnimations()
    {
        // Update movement speed parameter
        float speed = isMoving ? moveSpeed : 0f;
        animator.SetFloat("Speed", speed);
        
        // Update movement direction
        animator.SetFloat("DirectionX", moveDirection.x);
        animator.SetFloat("DirectionZ", moveDirection.z);
        
        // Apply movement
        if (isMoving)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
            
            // Rotate towards movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
    
    public void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
    
    public void PlaySpellAnimation()
    {
        animator.SetTrigger("Spell");
    }
    
    public void PlayDamagedAnimation()
    {
        animator.SetTrigger("Damaged");
    }
    
    public void PlayVictoryAnimation()
    {
        animator.SetTrigger("Victory");
    }
    
    public void PlayDefeatAnimation()
    {
        animator.SetTrigger("Defeat");
    }
    
    public void PlayEmoteAnimation(string emoteName)
    {
        animator.SetTrigger(emoteName);
    }
}
