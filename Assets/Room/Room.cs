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
    private List<Attackable> attackablesInRoom;

    void Start()
    {
        roomTimer = maxTimerSeconds;
        
    }

    void Update()
    {
        roomTimer -= Time.deltaTime;

        if (roomTimer <= 0f)
        {
            HandleRoomTimerFinished();
        }
    }

    void HandleRoomTimerFinished()
    {
        bool playerInside = Physics2D.OverlapBox(
            roomCollider.bounds.center,
            roomCollider.bounds.extents,
            0f,
            playerLayer
        );

        if (playerInside)
        {
            RoomManager.Instance.GameOver();
        }
    }

}
