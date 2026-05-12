using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 7;
    [SerializeField] bool isLookingRight;

    [Header("Climbing")]
    [SerializeField] float climbSpeed = 4f;

    [Header("Jumping")]
    [SerializeField] float jumpTakeOffSpeed = 7;

    [Header("BaseClasses")]
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer boomBoxSprite;
    [SerializeField] Animator animator;

    [Header("Input")]
    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction jumpAction;

    [Header("Groundcheck")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector2 groundCheckBoxSize = new Vector2(0.8f, 0.1f);
    [SerializeField] float groundCheckDistance = 0.05f;
    [SerializeField] float groundCheckYOffset = 0.55f;

    float xInput;
    float yInput;

    bool isTouchingLadder;
    bool isClimbing;
    bool jump;
    bool isGrounded;
    public bool canMove = true;

    private int facingDirection = 1;
    public int FacingDirection => facingDirection; // added this for knockback direction

    void Awake()
    {
        // Initialise input
        moveAction = InputSystem.actions.FindAction("Player/Move");
        jumpAction = InputSystem.actions.FindAction("Player/Jump");

        moveAction.Enable();
        jumpAction.Enable();
    }

    void Update()
    {
        CheckGrounded();

        // Collect input
        xInput = moveAction.ReadValue<Vector2>().x;
        yInput = moveAction.ReadValue<Vector2>().y;

        HandleAnimation(xInput, yInput);
        ComputeLookDirection(xInput);
        FlipSprite();
        
       
            DetectClimbing(yInput);
            DetectJumping();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            ComputeVelocity(xInput, yInput);
        }
    }

    private void DetectJumping()
    {
        if (isGrounded && !isTouchingLadder && jumpAction.WasPressedThisFrame())
        {
            jump = true;
        }
    }

    private void DetectClimbing(float yInput)
    {
        if (isTouchingLadder && Mathf.Abs(yInput) > 0.1f)
        {
            isClimbing = true;
        }
    }

    void ComputeVelocity(float moveIntendX, float moveIntendY)
    {
        Vector2 targetVelocity = rigidBody.linearVelocity;

        if (isClimbing)
        {
            targetVelocity.y = moveIntendY * climbSpeed;
        }
        else if (jump && isGrounded)
        {
            animator.SetTrigger("JumpTrigger");
            targetVelocity.y = jumpTakeOffSpeed;
            jump = false;
        }

        targetVelocity.x = moveIntendX * moveSpeed;
        rigidBody.linearVelocity = targetVelocity;
    }

    void CheckGrounded()
    {
        Vector2 origin = (Vector2)transform.position + Vector2.down * (groundCheckYOffset * transform.lossyScale.y);

        RaycastHit2D hit = Physics2D.BoxCast(
            origin,
            groundCheckBoxSize,
            0f,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        isGrounded = hit.collider != null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;

        Vector2 origin = (Vector2)transform.position + Vector2.down * (groundCheckYOffset * transform.lossyScale.y);
        Vector2 castCenter = origin + Vector2.down * groundCheckDistance;

        Gizmos.DrawWireCube(castCenter, groundCheckBoxSize);
    }

    public void EnteredLadder()
    {
        isTouchingLadder = true;
        rigidBody.gravityScale = 0f;
        rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, 0f);
    }

    public void ExitLadder()
    {
        isTouchingLadder = false;
        isClimbing = false;
        rigidBody.gravityScale = 1f;
    }

    void ComputeLookDirection(float moveInput)
    {
        if (moveInput > 0.01f)
        {
            facingDirection = 1; // added this for knockback direction
            isLookingRight = true;
        }
        else if (moveInput < -0.01f)
        {
            facingDirection = -1;
            isLookingRight = false;
        }
    }

    void FlipSprite()
    {
        if (isLookingRight)
        {
            spriteRenderer.flipX = false;
            boomBoxSprite.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
            boomBoxSprite.flipX = true;
        }
    }

    void HandleAnimation(float xInput, float yInput)
    {
        animator.SetBool("IsRunning", Mathf.Abs(xInput) > 0.01f && isGrounded);
        animator.SetBool("IsIdle", Mathf.Abs(xInput) == 0f && isGrounded);

        animator.SetBool("IsClimbing", isClimbing);

        if (Mathf.Abs(xInput) > 0.01f || Mathf.Abs(yInput) > 0.01f)
        {
            animator.SetFloat("ClimbSpeed", 1f);
        }
        else
        {
            animator.SetFloat("ClimbSpeed", 0f);
        }
    }

    public bool IsLookingRight { get { return isLookingRight; } }
}
