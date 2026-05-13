using UnityEngine;

public class CreepEnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject creepPrefab;
    [SerializeField] private Transform spawnPoint; 
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !triggered)
        {
            triggered = true;
            Instantiate(creepPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
