using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachoTrapRoomLogic : MonoBehaviour
{
    [SerializeField] List<GameObject> enemiesInTrapRoom;
    [SerializeField] GameObject door;
    [SerializeField] float doorSlideHeight = 3f;
    [SerializeField] float doorSlideSpeed = 2f;
    private bool doorOpened = false;


    void Update()
    {
        if (doorOpened) return;

        // removing all enemies in list that are nullk
        enemiesInTrapRoom.RemoveAll(enemy => enemy == null);

        if (enemiesInTrapRoom.Count == 0 && doorOpened == false)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        doorOpened = true;
        StartCoroutine(SlideDoorUp());
    }

    IEnumerator SlideDoorUp()
    {
        Vector3 startPos = door.transform.position;
        Vector3 targetPos = startPos + new Vector3(0f, doorSlideHeight, 0f);

        while (door.transform.position != targetPos)
        {
            door.transform.position = Vector3.MoveTowards(
                door.transform.position,
                targetPos,
                doorSlideSpeed * Time.deltaTime
            );
            yield return null;
        }
    }
}
