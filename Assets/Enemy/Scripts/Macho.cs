using UnityEngine;

public class Macho : MonoBehaviour
{
    bool IsCollided;
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
            IsCollided = true;
            animator.SetBool("IsCollided", true);
        }
    }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                IsCollided = false;
                animator.SetBool("IsCollided", false);
            }
        }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsCollided = true;
            animator2.SetBool("IsCollided", true);
        }
    }
     private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsCollided = false;
            animator2.SetBool("IsCollided", false);
        }
    }

}
