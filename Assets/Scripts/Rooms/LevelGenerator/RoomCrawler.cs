using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCrawler : MonoBehaviour
{
    public Vector2Int position { get; set; }

    public RoomCrawler(Vector2Int initPos)
    {
        position = initPos;
    }

    public Vector2Int Move(Dictionary<Direction, Vector2Int> movementMap)
    {
        Direction toMove = (Direction) Random.Range(0, movementMap.Count);
        position = movementMap[toMove];
        return position;
    }

}
