using System.Collections;
using UnityEngine;

public class OilThrowerEnemy : MonoBehaviour
{
    [Header("PlayerDetection")]
    [SerializeField] Transform player;
    [SerializeField] float detectionRange = 5f;
    [SerializeField] float throwRange = 3f;

    [Header("OilObject")]
    [SerializeField] private GameObject oilPuddlePrefab;
    [SerializeField] private Transform spawnPos;

    [Header("Movement")]
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;

    private bool oilThrown = false;
    private bool isRunningAway = false;
    private float moveDir;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; 
        }
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        float distanceFromPlayer = Mathf.Abs(transform.position.x - player.position.x);
       
        if (isRunningAway) // don't chase anymore
        {
            rb.linearVelocity = new Vector2(moveDir * runSpeed, rb.linearVelocity.y);
            return;
        }

        if (distanceFromPlayer <= detectionRange)
        {
            ChasePlayer();
            if (distanceFromPlayer <= throwRange && !oilThrown) 
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
                ThrowOil();
                
            }
        }
           
    }

    private void ChasePlayer()
    {
        float direction = (player.position.x - transform.position.x);

        if (direction < 0f)
        {
            moveDir = -1f;
        }
        else
        {
            moveDir = 1f;
        }
        rb.linearVelocity = new Vector2(moveDir * runSpeed, rb.linearVelocity.y);
        
        if (direction > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    private void ThrowOil()
    {
        GameObject puddle = Instantiate(oilPuddlePrefab, spawnPos.position, Quaternion.identity);

        oilThrown = true;
        RunAway();
    }

    private void RunAway()
    {
        isRunningAway = true;
        // Run opposite direction from player
        float direction = transform.position.x - player.position.x;
        if (direction < 0f)
        {
            moveDir = -1f;
        }
        else
        {
            moveDir = 1f;
        }
        if (moveDir > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        StartCoroutine(KillsHimself());
    }

    IEnumerator KillsHimself()
    {
        yield return new WaitForSeconds(10f);
        rb.linearVelocity = Vector2.zero;
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, throwRange);
    }
}