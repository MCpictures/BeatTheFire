using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 7;

    [SerializeField]
    float jumpTakeOffSpeed = 7;

    [SerializeField]
    float gravity = 9.81f;

    [SerializeField]
    Rigidbody2D rigidBody;

    [SerializeField]
    InputAction m_MoveAction;
    [SerializeField]
    InputAction m_JumpAction;

    bool jump;
    bool isJumping;

    void Awake()
    {
        m_MoveAction = InputSystem.actions.FindAction("Player/Move");
        m_JumpAction = InputSystem.actions.FindAction("Player/Jump");

        m_MoveAction.Enable();
        m_JumpAction.Enable();
    }

    void Update()
    {
        float xInput = m_MoveAction.ReadValue<Vector2>().x;

        ComputeVelocity(xInput);

        if (!isJumping && m_JumpAction.WasPressedThisFrame())
            jump = true;
    }

    void ComputeVelocity(float moveIntendX)
    {
        Vector2 targetVelocity = rigidBody.linearVelocity;

        if (jump && Mathf.Abs(targetVelocity.y) < 0.01f)
        {
            targetVelocity.y = jumpTakeOffSpeed;
            jump = false;
        }

        targetVelocity.y += gravity * Time.deltaTime;

        targetVelocity.x = moveIntendX * moveSpeed;

        rigidBody.linearVelocity = targetVelocity;
    }
}
