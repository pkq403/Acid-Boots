using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{   
    // Room Camera
    [SerializeField] private float smoothTime;
    private float[] currentPos = new float[2];
    private Vector3 velocity = Vector3.zero;

    // Follow player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float ycameraOffset;
    [Header("Unused lookahead")]
    private float lookAhead;


    private void Update()
    {   // Traditional camera movement (moves by room where u are)
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPos[0], transform.position.y, transform.position.z), ref velocity, speed);

        // Follow player, a bit more complex camera movement (lookAheadDistance)
        // it gets to the left, or to the right depending on where is the player facing
        //transform.position = new Vector3(player.position.x + lookAhead, player.position.y + ycameraOffset, transform.position.z);
        //lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * speed);

        // Just Follows player, with no lookAhead
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, player.position.y + ycameraOffset, transform.position.z), ref velocity, smoothTime);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPos[0] = _newRoom.position.x;
        currentPos[1] = _newRoom.position.y;
    }
}
