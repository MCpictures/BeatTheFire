using System;
using UnityEngine;

public class Room : MonoBehaviour
{

    // This script handles the timer of a specific room
    [SerializeField] private float maxSeconds = 10f;
    private float roomTimer = 0f;
    private bool isRoomTimerFinished;


    void Update()
    {
        roomTimer += Time.deltaTime;

        if (roomTimer <= 0f)
        {
            HandleRoomTimerFinished();
        }
    }

    void HandleRoomTimerFinished()
    {
        isRoomTimerFinished = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isRoomTimerFinished && collider)
        {
        //    Debug.Log();
        }
    }



}
