using UnityEngine;

public class RotationForSpinEnemy : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 90f;

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
