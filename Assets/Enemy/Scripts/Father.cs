using UnityEngine;

public class Father : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private Transform player;
    [SerializeField] private float detectionRange = 8f;
    [SerializeField] private float shootInterval = 2f;
    private float timer;


    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            timer += Time.deltaTime;
            if (timer > shootInterval)
            {
                timer = 0;
                Shoot();
            }
        }
    }
    private void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

    }
}
