using UnityEngine;

public class EnemyKnockbackToCharacter : MonoBehaviour
{
    [SerializeField] private float knockbackStrength;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<KnockbackOnCharacter>().DoKnockback(knockbackStrength);

        }
    }
}
