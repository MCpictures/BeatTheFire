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

        // float distanceToPlayer = Vector2.Distance(transform.position, player.position); Had a bug because y was included in the distance
        float distanceToPlayer = Mathf.Abs(transform.position.x - player.position.x);

        if (distanceToPlayer <= detectionRange)
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }

        if (playerDetected)
        {
            ChasePlayer();
            animator.SetBool("IsRunning", true);
        } else
        {
            animator.SetBool("IsRunning", false);
        }

    }

    private void ChasePlayer()
    {
        // Calculate direction towards the target
        float direction = player.position.x - transform.position.x;

        // Move the enemy towards the target
        Vector2 newPosition = Vector2.MoveTowards(rigidBody.position, player.position, moveSpeed * Time.fixedDeltaTime);
        rigidBody.MovePosition(newPosition);

        float moveDir = direction > 0 ? 1f : -1f;
        if (moveDir > 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}