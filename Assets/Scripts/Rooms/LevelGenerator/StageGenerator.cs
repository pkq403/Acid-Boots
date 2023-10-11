using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    public StageGenerationData roomGenerarionData;
    private List<Vector2Int> stageRooms;

    private void Start()
    {
        stageRooms = RoomCrawlerController.GenerateStage(roomGenerarionData);
        SpawnRooms(stageRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        StageController.instance.LoadRoom("Start", 0, 0);
        foreach (Vector2Int roomLocation in rooms)
        {
            StageController.instance.LoadRoom("Empty", roomLocation.x, roomLocation.y);
        }
    }
}
