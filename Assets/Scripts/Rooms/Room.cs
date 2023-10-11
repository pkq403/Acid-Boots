using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Room : MonoBehaviour
{
    // Room properties
    public int Width;
    public int Height;

    public int X;
    public int Y;

    [SerializeField] private GameObject[] enemies;
    private Vector3[] initialPosition;

    private void Awake()
    {
        initialPosition = new Vector3[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            initialPosition[i] = enemies[i].transform.position;
        }
    }
    void Start()
    {
        if (StageController.instance == null)
        {
            Debug.Log("Pressed play in the wrong scene");
        }
        Debug.Log("prefailed!");
        StageController.instance.RegisterRoom(this);
    }
    
    public Vector3 getRoomCenter()
    {
        return new Vector3(X * Width, Y * Height);
    }
    void onDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }

    public void ActivateRoom(bool _status)
    {
        // Activate/deactivate enemies (activa o desactiva enemigos segun sales de la sala)
        for (int i = 0;i < enemies.Length;i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPosition[i];
            }
        }
    }
}
