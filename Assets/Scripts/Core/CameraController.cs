using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Singleton
    public static CameraController instance;

    // New Room Camera Movement + player follow by the camera
    /*
     * I need to make a camera than change the room when the player gets into
     * a new one, but the camera should follow the player in the room in which
     * he is
     */
    public Room currRoom;
    public float moveSpeedWhenRoomChange;
    public bool inRoom = true; // if the player is in a Room triggers the camera player follow-up

    // Old Room Camera Movement
    [SerializeField] private float smoothTime;
    private float[] currentPos = new float[2];
    private Vector3 velocity = Vector3.zero;

    // Follow player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float ycameraOffset;
    [Header("Unused lookahead")]
    private float lookAhead;

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {   // Traditional camera movement (moves by room where u are)
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPos[0], transform.position.y, transform.position.z), ref velocity, speed);

        // Follow player, a bit more complex camera movement (lookAheadDistance)
        // it gets to the left, or to the right depending on where is the player facing
        //transform.position = new Vector3(player.position.x + lookAhead, player.position.y + ycameraOffset, transform.position.z);
        //lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * speed);

        Vector3 target;
        // Follows the player when in a room
        if (inRoom)
        {
            target = new Vector3(player.position.x, player.position.y + ycameraOffset, transform.position.z);
        }
        else // Between Rooms, the camera moves to a reference point in the room the player is moving towards
        {
            target = new Vector3(currentPos[0], currentPos[1], transform.position.z);
        }
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPos[0] = _newRoom.position.x;
        currentPos[1] = _newRoom.position.y;
    }
}
