using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private Room[] rooms;
    public float globalTimer = 0;
    private bool isGameOver = false;

    public static RoomManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning("there should only be one room manager in a scene");
            return;
        }
        Instance = this;
    }
    void Start()
    {
        rooms = FindObjectsByType<Room>();
        for (int i = 0; i < rooms.Count(); i++)
        {
            globalTimer += rooms[i].maxTimerSeconds;
        }

        globalTimer = globalTimer / 2;
        print("global timer: " + globalTimer);
    }

    void Update()
    {
        globalTimer -= Time.deltaTime;

        if (globalTimer <= 0f)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            print("game over");
            isGameOver = true;
        }
    }

}
