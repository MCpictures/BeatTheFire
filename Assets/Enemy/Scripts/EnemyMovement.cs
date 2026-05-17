using UnityEngine;

public class EnemyChaseAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float detectionRange = 5f;
    [SerializeField] float detectionHeight = 1f;

    [Header("ExternClasses")]
    [SerializeField] Transform player;

    [Header("BaseClasses")]
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Attackable attackable;

    void FixedUpdate()
    {
        if (player == null) return;

        float distanceFromPlayerX = Mathf.Abs(transform.position.x - player.position.x);
        float distanceFromPlayerY = Mathf.Abs(transform.position.y - player.position.y);

        if (distanceFromPlayerX <= detectionRange && distanceFromPlayerY <= detectionHeight )
        {
            ChasePlayer();
            animator.SetBool("IsRunning", true);
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
        Gizmos.DrawWireCube(transform.position, new Vector3(detectionRange * 2, detectionHeight * 2, 0));
        Gizmos.color = Color.yellow;
    }
}