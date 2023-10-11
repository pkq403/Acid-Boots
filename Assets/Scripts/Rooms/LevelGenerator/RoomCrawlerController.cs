using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum Direction
{
    top = 0,
    bottom = 1,
    left = 2,
    right = 3 
}

public class RoomCrawlerController : MonoBehaviour
{
    public static List<Vector2Int> positionsVisited = new List<Vector2Int>();
    private static readonly Dictionary<Direction, Vector2Int> movementMap = new Dictionary<Direction, Vector2Int>
    {
        {Direction.top, Vector2Int.up},
        {Direction.bottom, Vector2Int.down},
        {Direction.left, Vector2Int.left},
        {Direction.right, Vector2Int.right}
        
    };

    public static List<Vector2Int> GenerateStage(StageGenerationData generationData)
    {
        // Un Room Crawler es un generador que hace su propio camino de Rooms
        // Por eso hay una lista de DungeonCrawlers
        List<RoomCrawler> dungeonCrawlers = new List<RoomCrawler>();


        for (int i = 0; i < generationData.numberOfCrawlers; i++)
        {
            dungeonCrawlers.Add(new RoomCrawler(Vector2Int.zero));

        }

        int iterations = Random.Range(generationData.iterationMin, generationData.iterationMax);

        for (int i = 0; i < iterations; i++)
        {
            foreach (RoomCrawler dungeonCrawler in dungeonCrawlers)
            {
                Vector2Int newPos = dungeonCrawler.Move(movementMap);
                positionsVisited.Add(newPos);

            }
        }

        return positionsVisited;
    }
}
