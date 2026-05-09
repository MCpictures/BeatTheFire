using UnityEngine;

public enum AttackColliderPosition
{
    Left,
    Right
}

public class CharacterAttackCollider : MonoBehaviour
{
    [SerializeField] CharacterAttack characterAttack;
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
            characterAttack.AttackableLeft(attackable, colliderPosition);
        }
    }
}
