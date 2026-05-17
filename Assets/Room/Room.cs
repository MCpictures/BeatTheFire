using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Room : MonoBehaviour
{

    // This script handles the timer of a specific room
    [SerializeField] float maxTimerSeconds = 10f;
    [SerializeField] float roomDieSeconds = 2f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private BoxCollider2D roomCollider;
    [SerializeField] private float attackablePenalty = 1f; // amount of time lost when attacking an attackable
    private bool hasPlayerEnteredRoom = false;
    [SerializeField] private List<ParticleSystem> fireParticles;
    [SerializeField] private UnityEngine.UI.Image RedOverlay;
    private float exitRoomGraceTimer;
    private float roomTimer;
    [SerializeField] private bool resetedRedScreen = true;

    void Start()
    {
        roomTimer = maxTimerSeconds;
        exitRoomGraceTimer = roomDieSeconds;
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
        if(roomTimer > 0)
        {
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
                ShowFireSpread();
            }
        }
        else
        {
            if(CheckIfPlayerIsInRoom())
            {
                exitRoomGraceTimer -= Time.deltaTime;
                HandleRedOverlay();
            }
            else if(!resetedRedScreen)
            {
                exitRoomGraceTimer = roomDieSeconds;
                if (RedOverlay != null)
                    RedOverlay.color = new Color(1f, 0f, 0f, 0f);
                resetedRedScreen = true;
            }

            if (exitRoomGraceTimer <= 0f)
            {
                HandleRoomTimerFinished();
            }
        }
    }

    void HandleRoomTimerFinished()
    {
        if (CheckIfPlayerIsInRoom())
        {
            RoomManager.Instance.GameOver();
        }
    }

    void HandleAttackableAttacked(Attackable attackable)
    {
        if (!hasPlayerEnteredRoom) return;

        // check if this attackable is inside the room
        Collider2D attackableCollider = attackable.GetComponentInChildren<Collider2D>();
        if (attackableCollider == null) return;

        bool isInsideRoom = roomCollider.bounds.Contains(attackableCollider.bounds.center);

        if (isInsideRoom)
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

    void ShowFireSpread()
    {
        float ratio = roomTimer / maxTimerSeconds;

        int smallFiresCount = Mathf.RoundToInt(fireParticles.Count * 0.25f);
        int MediumFiresCount = Mathf.RoundToInt(fireParticles.Count * 0.50f);
        int HighFiresCount = Mathf.RoundToInt(fireParticles.Count * 0.75f);
        int allFiresCount = fireParticles.Count;

        int targetFiresCount = 0;

        if (ratio <= 0.1f)
        {
            targetFiresCount = allFiresCount;
        }
        else if (ratio <= 0.25f)
        {
            targetFiresCount = HighFiresCount;
        }
        else if (ratio <= 0.50f)
        {
            targetFiresCount = MediumFiresCount;
        }
        else if (ratio <= 0.75f)
        {
            targetFiresCount = smallFiresCount;
        }


        for (int i = 0; i < fireParticles.Count; i++)
        {
            if (i < targetFiresCount)
            {
                if (!fireParticles[i].isPlaying)
                    fireParticles[i].Play();
            }
            else
            {
                if (fireParticles[i].isPlaying)
                    fireParticles[i].Stop();
            }
        }
    }

    void HandleRedOverlay()
    {
        if (RedOverlay != null)
        {
            resetedRedScreen = false;
            float timerDanger = 1f - (exitRoomGraceTimer / roomDieSeconds);
            RedOverlay.color = new Color(1f, 0f, 0f, timerDanger * 0.5f);
        }
    }
}
