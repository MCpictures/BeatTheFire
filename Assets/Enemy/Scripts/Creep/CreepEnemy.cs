using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreepEnemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer enemyboomboxSpriteRenderer; 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D groundCheckCollider;

    [Header("Movement")]
    [SerializeField] private float fallSpeed = 2f;      
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float runAwayDuration = 5f;

    private SpriteRenderer playerBoomboxSprite;
    SpriteRenderer[] sprites;
    private Transform player;
    private bool isGrounded = false;
    private bool hasStolenBoombox = false;
    private bool isRunningAway = false;
    private float runAwayDir = 1f;


    private float moveDir;

    private GameObject playerObj;
    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            
        }

        enemyboomboxSpriteRenderer.enabled = false; 
        rb.gravityScale = 0f; 
    }

    private void FixedUpdate()
    {
        if (!isGrounded)
        {
            rb.linearVelocity = new Vector2(0, -fallSpeed); 
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // for fall speed
        } 

        if (hasStolenBoombox && isRunningAway)
        {
            rb.linearVelocity = new Vector2(runAwayDir * runSpeed, rb.linearVelocity.y);
            FlipSprite(runAwayDir);
        }
        else if (isGrounded && !hasStolenBoombox)
        {
            ChasePlayer();
        }
    }

    

    private void ChasePlayer()
    {
        float direction = player.position.x - transform.position.x;
        moveDir = direction > 0 ? 1f : -1f;
        rb.linearVelocity = new Vector2(moveDir * runSpeed, rb.linearVelocity.y);
        FlipSprite(moveDir);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Player") && !hasStolenBoombox)
        {
            SpriteRenderer[] sprites = collision.transform.root.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in sprites)
            {
                if (sr.gameObject.name == "BoomBox")
                {
                    playerBoomboxSprite = sr;
                    break;
                }
            }
                StealBoombox();
            
        }
    }

    private void StealBoombox()
    {
        hasStolenBoombox = true;

        playerBoomboxSprite.enabled = false;

        enemyboomboxSpriteRenderer.enabled = true;

       float runDir = transform.position.x - player.position.x;
         runAwayDir = runDir > 0 ? 1f : -1f;
        FlipSprite(runAwayDir);
        isRunningAway = true;

        StartCoroutine(DespawnAfterRunning());
    }

    private void FlipSprite(float direction)
    {
        if (direction > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    IEnumerator DespawnAfterRunning()
    {
        yield return new WaitForSeconds(runAwayDuration);
        Destroy(gameObject);
    }
}
