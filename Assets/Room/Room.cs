using System;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    // This script handles the timer of a specific room
    public float maxTimerSeconds = 10f;
    private float roomTimer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private BoxCollider2D roomCollider;
    [SerializeField] private float attackablePenalty = 1f; // amount of time lost when attacking an attackable
    private bool hasPlayerEnteredRoom = false;
    private List<Attackable> attackablesInRoom = new List<Attackable>();

    void Start()
    {
        roomTimer = maxTimerSeconds;
        CheckForAttackablesInRoom();

    }

    void OnEnable()
    {
        Attackable.OnAttackableAttacked += HandleAttackableAttacked;
    }

    void OnDisable()
    {
        Attackable.OnAttackableAttacked -= HandleAttackableAttacked;
    }


    void Update()
    {
        // only updating room timer if the player has entered the room before
        if (!hasPlayerEnteredRoom)
        {
            if (CheckIfPlayerIsInRoom())
            {
                hasPlayerEnteredRoom = true;
            }
        }
        else
        {
            roomTimer -= Time.deltaTime;
        }


        if (roomTimer <= 0f)
        {
            HandleRoomTimerFinished();
            roomTimer = 0f;
        }
    }

    void HandleRoomTimerFinished()
    {
        if (CheckIfPlayerIsInRoom())
        {
            RoomManager.Instance.GameOver();
        }
    }

    void CheckForAttackablesInRoom()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(
        roomCollider.bounds.center,
        roomCollider.bounds.size,
        0f
        );

        foreach (var hit in hits)
        {
            Attackable attackable = hit.GetComponentInParent<Attackable>();
            if (attackable != null && !attackablesInRoom.Contains(attackable))
            {
                attackablesInRoom.Add(attackable);
            }
        }
    }

    void HandleAttackableAttacked(Attackable attackable)
    {
        if (hasPlayerEnteredRoom && attackablesInRoom.Contains(attackable))
        {
            roomTimer = Mathf.Max(0f, roomTimer - attackablePenalty);
            RoomManager.Instance.globalTimer = Mathf.Max(0f, RoomManager.Instance.globalTimer - attackablePenalty);
            print("Room timer updated from attackable being attacked. It is now: " + roomTimer + ". Global room timer: " + RoomManager.Instance.globalTimer);
        }
    }

    private bool CheckIfPlayerIsInRoom()
    {
        bool playerInside = Physics2D.OverlapBox(
            roomCollider.bounds.center,
            roomCollider.bounds.size,
            0f,
            playerLayer
        );

        return playerInside;
    }


}
