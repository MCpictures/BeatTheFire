using UnityEngine;

public enum AttackColliderPosition
{
    Left,
    Right
}

// Helper for the attack collider functions on the character
public class CharacterAttackCollider : MonoBehaviour
{
    [Header("BaseClasses")]
    [SerializeField] CharacterAttack characterAttack;

    [Header("Position information")]
    [SerializeField] AttackColliderPosition colliderPosition;

    void OnTriggerEnter2D(Collider2D other)
    {
        Attackable attackable = other.gameObject.GetComponentInParent<Attackable>();
        if(attackable != null) 
        {
            characterAttack.AttackableEntered(attackable, colliderPosition);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Attackable attackable = other.gameObject.GetComponentInParent<Attackable>();
        if (attackable != null)
        {
            characterAttack.AttackableExits(attackable, colliderPosition);
        }
    }
}
