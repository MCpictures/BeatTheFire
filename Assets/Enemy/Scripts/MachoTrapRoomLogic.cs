using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachoTrapRoomLogic : MonoBehaviour
{
    [SerializeField] List<GameObject> enemiesInTrapRoom;
    [SerializeField] GameObject door;
    [SerializeField] float doorSlideHeight = 3f;
    [SerializeField] float doorSlideSpeed = 2f;
    [SerializeField] private SpriteRenderer playerBoomboxSprite;
    [SerializeField] private SpriteRenderer pickUpBoomboxSprite;
    private bool doorOpened = false;
    private bool isBoomBoxOverlapped = false;
    [SerializeField] private LayerMask playerLayer;
    private BoxCollider2D boomBoxPickupCollider;

    void Start()
    {
        boomBoxPickupCollider = pickUpBoomboxSprite.GetComponent<BoxCollider2D>();
        pickUpBoomboxSprite.enabled = false;
    }

    void Update()
    {
        if (!doorOpened)
        {
            enemiesInTrapRoom.RemoveAll(enemy => enemy == null);
            if (enemiesInTrapRoom.Count == 0)
            {
                OpenDoor();
            }
        }

        if (doorOpened && !isBoomBoxOverlapped)
        {
            isBoomBoxOverlapped = Physics2D.OverlapBox(
                boomBoxPickupCollider.bounds.center,
                boomBoxPickupCollider.bounds.size,
                0f,
                playerLayer
            );

            if (isBoomBoxOverlapped)
            {
                PickupBoomBox();
            }
        }

    }

    void OpenDoor()
    {
        doorOpened = true;
        pickUpBoomboxSprite.enabled = true;
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

    void PickupBoomBox()
    {
        pickUpBoomboxSprite.enabled = false;
        playerBoomboxSprite.enabled = true;
    }
}
