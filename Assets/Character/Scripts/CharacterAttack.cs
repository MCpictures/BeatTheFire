using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] CharacterMovement characterMovement;
    [SerializeField] InputAction attackAction;

    List<Attackable> rightAttackables = new List<Attackable>();
    List<Attackable> leftAttackables = new List<Attackable>();

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
        if(attackAction.WasPressedThisFrame())
        {
            if(characterMovement.IsLookingRight)
            {
                foreach (var attackable in rightAttackables)
                {
                    attackable.Attacked();
                }
            }
            else
            {
                foreach (var attackable in leftAttackables)
                {
                    attackable.Attacked();
                }
            }
        }
    }

    public void AttackableEntered(Attackable attackableIn, AttackColliderPosition attackablePosition)
    {
        switch(attackablePosition)
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
}
