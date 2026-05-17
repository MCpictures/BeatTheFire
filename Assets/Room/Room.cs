using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{

    // This script handles the timer of a specific room
    public float maxTimerSeconds = 10f;
    private float roomTimer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private BoxCollider2D roomCollider;
    [SerializeField] private float attackablePenalty = 1f; // amount of time lost when attacking an attackable
    private bool hasPlayerEnteredRoom = false;
    [SerializeField] private List<ParticleSystem> fireParticles;
    [SerializeField] private Image RedOverlay;
    private float exitRoomGraceTimer;
    void Start()
    {
        roomTimer = maxTimerSeconds;

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
            ShowFireSpread();
            if (!CheckIfPlayerIsInRoom())
            {
                if (RedOverlay != null)
                    RedOverlay.color = new Color(1f, 0f, 0f, 0f);
            }
        }

        if (!CheckIfPlayerIsInRoom())
        {
            exitRoomGraceTimer = 2f;
        }
        else
        {
            exitRoomGraceTimer -= Time.deltaTime;
        }


        if (roomTimer <= 0f && exitRoomGraceTimer <= 0f)
        {
            HandleRoomTimerFinished();
            roomTimer = 0f;
            exitRoomGraceTimer = 0f;
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

        if (RedOverlay != null)
        {
            float timerDanger = 1f - ratio;
            // float graceDanger = 1f - (exitRoomGraceTimer / 2.0f);
            // float danger = Math.Min(timerDanger, graceDanger);
            RedOverlay.color = new Color(1f, 0f, 0f, timerDanger * 0.5f);
        }
    }


}
