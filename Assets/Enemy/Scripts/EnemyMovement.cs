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

        if (playerDetected)
        {
            ChasePlayer();
            animator.SetBool("IsRunning", true);
        }
       
    }

    private void ChasePlayer()
    {
        // Calculate direction towards the target
        Vector2 direction = (player.position - transform.position).normalized;
        spriteRenderer.flipX = direction.x > 0;
        // Move the enemy towards the target
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

   

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}