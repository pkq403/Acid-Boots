using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;

    [SerializeField] Door leftDoor;
    [SerializeField] Door rightDoor; 
    void Awake()
    {
        leftDoor = transform.Find("RightDoor").GetComponent<Door>();
        rightDoor = transform.Find("LeftDoor").GetComponent<Door>();

        leftDoor.nextRoom = this.nextRoom;
        leftDoor.previousRoom = this.previousRoom;
        rightDoor.nextRoom = this.previousRoom;
        rightDoor.previousRoom = this.nextRoom;
    }
}
