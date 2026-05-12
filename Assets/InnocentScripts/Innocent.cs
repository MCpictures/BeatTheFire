using UnityEngine;

public class Innocent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<InnocentActivator>(out InnocentActivator innocent))
            {
                innocent.ActivateItemDisplay();
            }

            Destroy(gameObject);
        }
    }
}