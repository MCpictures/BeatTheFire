using UnityEngine;

public class Macho : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Animator animator2;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("IsCollided", true);
        }
    }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                animator.SetBool("IsCollided", false);
            }
        }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator2.SetBool("IsCollided", true);
        }
    }
     private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator2.SetBool("IsCollided", false);
        }
    }

}
