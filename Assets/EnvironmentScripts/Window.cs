using UnityEngine;

public class Window : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<InnocentActivator>(out InnocentActivator innocent))
            {
                innocent.DeactivateItemDisplay();
            }
        }
    }
}
