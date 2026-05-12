using UnityEngine;

public class CharacterCollider : MonoBehaviour
{ 
    [Header("Layer information")]
    [SerializeField] LayerMask ladderLayer;

    [Header("BaseClasses")]
    [SerializeField] CharacterMovement characterMovement;

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
