using UnityEngine;

[CreateAssetMenu(fileName ="RoomGenerationData.asset", menuName ="RoomGenerationData/Room_Data")]
public class StageGenerationData : ScriptableObject
{
    public int numberOfCrawlers;
    public int iterationMin;
    public int iterationMax;
}
