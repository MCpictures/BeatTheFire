using System.Collections;
using UnityEngine;

public class OilPuddle : MonoBehaviour
{
    [SerializeField] Vector3 fullSize = new Vector3(0f, 0f, 0f);
    [SerializeField] private float oilSpreadDuration = 0.5f;
    [SerializeField] private float speed = 8f;
    private GameObject player;

    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * speed;
    }
    public void StartSpread()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(SpreadCoroutine());
    }

    IEnumerator SpreadCoroutine()
    {

        float time = 0f;
        while (time < oilSpreadDuration)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, fullSize, time / oilSpreadDuration);
            yield return null;
        }
        transform.localScale = fullSize;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            StartSpread();
        }
        
        if (collision.CompareTag("Player"))
        {
            CharacterMovement player = collision.GetComponentInParent<CharacterMovement>();
            if (player != null) player.isSlipping = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CharacterMovement player = collision.GetComponentInParent<CharacterMovement>();
            if (player != null) player.isSlipping = false;
        }
    }
}