using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAttack : MonoBehaviour
{
    [Header("BaseClasses")]
    [SerializeField] CharacterMovement characterMovement;

    [Header("Input")]
    [SerializeField] InputAction attackAction;

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootForce = 10f;

    List<Attackable> rightAttackables = new List<Attackable>();
    List<Attackable> leftAttackables = new List<Attackable>();
    [SerializeField] Animator animator;

    void Awake()
    {
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
            Attack();
        }
    }

    void Attack()
    {
        bool isLookingRight = characterMovement.IsLookingRight;
        if (isLookingRight)
        {
            foreach (var attackable in new List<Attackable>(rightAttackables))
            {
                attackable.Attacked(isLookingRight);
            }
        }
        else
        {
            foreach (var attackable in new List<Attackable>(leftAttackables))
            {
                attackable.Attacked(isLookingRight);
            }
        }
        //ShootFire(isLookingRight);
    }

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

    public void AttackableEntered(Attackable attackableIn, AttackColliderPosition attackablePosition)
    {
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

    public void AttackableLeft(Attackable attackableIn, AttackColliderPosition attackablePosition)
    {
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
