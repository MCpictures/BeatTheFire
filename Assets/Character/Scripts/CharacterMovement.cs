using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 7;

    [SerializeField] float climbSpeed = 4f;

    [SerializeField] float jumpTakeOffSpeed = 7;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector2 groundCheckBoxSize = new Vector2(0.8f, 0.1f);
    [SerializeField] float groundCheckDistance = 0.05f;
    [SerializeField] float groundCheckYOffset = 0.55f;

    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction jumpAction;

    [SerializeField] bool isLookingRight;
    [SerializeField] Animator animator;

    bool isTouchingLadder;
    bool isClimbing;

    bool jump;
    bool isGrounded;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Player/Move");
        jumpAction = InputSystem.actions.FindAction("Player/Jump");

        moveAction.Enable();
        jumpAction.Enable();
    }

    void Update()
    {
        CheckGrounded();

        float xInput = moveAction.ReadValue<Vector2>().x;
        float yInput = moveAction.ReadValue<Vector2>().y;

        HandleAnimation();
        ComputeLookDirection(xInput);
        FlipSprite();

        if (isTouchingLadder && Mathf.Abs(yInput) > 0.1f)
        {
            isClimbing = true;
        }

        if (isGrounded && !isTouchingLadder && jumpAction.WasPressedThisFrame())
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        float xInput = moveAction.ReadValue<Vector2>().x;
        float yInput = moveAction.ReadValue<Vector2>().y;

        ComputeVelocity(xInput, yInput);
    }

    void ComputeVelocity(float moveIntendX, float moveIntendY)
    {
        Vector2 targetVelocity = rigidBody.linearVelocity;

        if (isClimbing)
        {
            targetVelocity.x = moveIntendX * moveSpeed;
            targetVelocity.y = moveIntendY * climbSpeed;
        }
        else
        {
            if (jump && isGrounded)
            {
                animator.SetTrigger("JumpTrigger");
                targetVelocity.y = jumpTakeOffSpeed;
                jump = false;
            }

            targetVelocity.x = moveIntendX * moveSpeed;
        }

        rigidBody.linearVelocity = targetVelocity;
    }

    void CheckGrounded()
    {
        Vector2 origin = (Vector2)transform.position + Vector2.down * groundCheckYOffset;

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

        Vector2 origin = (Vector2)transform.position + Vector2.down * groundCheckYOffset;
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
            isLookingRight = true;
        }
        else if (moveInput < -0.01f)
        {
            isLookingRight = false;
        }
    }

    void FlipSprite()
    {
        if (isLookingRight)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    void HandleAnimation()
    {
        float xInput = moveAction.ReadValue<Vector2>().x;
        float yInput = moveAction.ReadValue<Vector2>().y;

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
