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

    bool playerDetected = false;



    void FixedUpdate()
    {
        if (player == null) return;

        float distanceToPlayer2D = Vector2.Distance(transform.position, player.position);
        float distanceToPlayerX = Mathf.Abs(transform.position.x - player.position.x);

        playerDetected = distanceToPlayer2D <= detectionRange;
     

        if (playerDetected)
        {
            animator.SetBool("IsRunning", true);

            if (distanceToPlayerX > stopDistance)
            {
                ChasePlayer();
            }

            else
            {
                rigidBody.linearVelocity = new Vector2(0f, rigidBody.linearVelocity.y);
            }

        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

    }

    private void ChasePlayer()
    {
        float direction = player.position.x - transform.position.x;
        float moveDir = direction > 0 ? 1f : -1f;

        rigidBody.linearVelocity = new Vector2(moveDir * moveSpeed, rigidBody.linearVelocity.y);

        transform.localScale = new Vector3(
            moveDir > 0 ? -Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x),
            transform.localScale.y,
            transform.localScale.z
        );
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}