using UnityEngine;

public class CharacterCollider : MonoBehaviour
{
    [SerializeField] CharacterMovement characterMovement;
    [SerializeField] LayerMask ladderLayer;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & ladderLayer) != 0)
        {
            characterMovement.EnteredLadder();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & ladderLayer) != 0)
        {
            characterMovement.ExitLadder();
        }
    }
}
