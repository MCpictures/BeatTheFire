using UnityEngine;

public class Innocent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InnocentActivator innocent = collision.GetComponentInParent<InnocentActivator>();
            if (innocent && !innocent.IsHoldingInnocent)
            {
                innocent.ActivateItemDisplay();
                Destroy(gameObject);
            }
        }
    }
}