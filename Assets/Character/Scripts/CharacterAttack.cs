using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAttack : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] InputAction attackAction;

    [Header("BaseClasses")]
    [SerializeField] CharacterMovement characterMovement;
    [SerializeField] Animator animator;
    [SerializeField] Transform characterTransform;

    /* Under construction...
    [Header("Projectile")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform shootPoint;
    [SerializeField] float shootForce = 10f;
    */

    List<Attackable> rightAttackables = new();
    List<Attackable> leftAttackables = new();
    

    void Awake()
    {
        // Initiate input
        attackAction = InputSystem.actions.FindAction("Player/Attack");

        attackAction.Enable();
    }

    void Update()
    {
        CheckForAttack();
    }

    void CheckForAttack()
    {
        if (attackAction.WasPressedThisFrame())
        {
            HandleAnimation();
        }
    }
    
    // called in animation event
    public void Attack()
    {
        bool isLookingRight = characterMovement.IsLookingRight;
        if (isLookingRight)
        {
            foreach (var attackable in new List<Attackable>(rightAttackables))
            {
                attackable.Attacked(characterTransform);
            }
        }
        else
        {
            foreach (var attackable in new List<Attackable>(leftAttackables))
            {
                attackable.Attacked(characterTransform);
            }
        }
    }

    /* Under construction...
    private void ShootFire(bool isLookingRight)
    {
        GameObject projectile = Instantiate(
            projectilePrefab,
            shootPoint.position,
            Quaternion.identity
        );

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 direction = isLookingRight ? Vector2.right : Vector2.left;

            // Apply force
            rb.AddForce(direction * shootForce, ForceMode2D.Impulse);
        }
    }
    */

    public void AttackableEntered(Attackable attackableIn, AttackColliderPosition attackablePosition)
    {
        // Add attackable to array for multi attack if in range
        switch (attackablePosition)
        {
            case AttackColliderPosition.Right:
                rightAttackables.Add(attackableIn);
                break;
            case AttackColliderPosition.Left:
                leftAttackables.Add(attackableIn);
                break;
        }
    }

    public void AttackableExits(Attackable attackableIn, AttackColliderPosition attackablePosition)
    {
        // Remove attackable from array if out of range
        switch (attackablePosition)
        {
            case AttackColliderPosition.Right:
                rightAttackables.Remove(attackableIn);
                break;
            case AttackColliderPosition.Left:
                leftAttackables.Remove(attackableIn);
                break;
        }
    }

    void HandleAnimation()
    {
        animator.SetTrigger("AttackTrigger");
    }
}
