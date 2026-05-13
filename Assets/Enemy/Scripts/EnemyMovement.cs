using UnityEngine;

public class EnemyChaseAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float detectionRange = 8f;
    [SerializeField] float stopDistance = 0.5f;

    [Header("ExternClasses")]
    [SerializeField] Transform player;

    [Header("BaseClasses")]
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Attackable attackable;

    bool facingRight = false;
    bool playerDetected = false;

    void FixedUpdate()
    {
        if (player == null) return;

        // float distanceToPlayer = Vector2.Distance(transform.position, player.position); Had a bug because y was included in the distance
        float distanceToPlayer = Mathf.Abs(transform.position.x - player.position.x);

        playerDetected = distanceToPlayer <= detectionRange;

        if (playerDetected && distanceToPlayer > stopDistance)
        {
            ChasePlayer();
            animator.SetBool("IsRunning", true);
        }
        else
        {
            // Stop moving
            if (!attackable.IsKnockedBack) rigidBody.linearVelocity = new Vector2(0, rigidBody.linearVelocity.y);
            animator.SetBool("IsRunning", false);
        }
    }

    private void ChasePlayer()
    {
        if (attackable.IsKnockedBack) return; // Early return if the player is knocked back

        float direction = player.position.x - transform.position.x;
        float moveDir = Mathf.Sign(direction);

        rigidBody.linearVelocity = new Vector2(moveDir * moveSpeed, rigidBody.linearVelocity.y);

        // Flip sprite to face player
        if (moveDir > 0 && !facingRight) Flip();
        else if (moveDir < 0 && facingRight) Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        if (facingRight)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}