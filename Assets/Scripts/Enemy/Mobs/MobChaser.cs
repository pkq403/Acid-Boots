using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobChaser : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float initialDelay;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float minDistance;
    private float chaseCooldown;

    // Update is called once per frame
    void Update()
    {
        if (chaseCooldown < initialDelay)
        {
            chaseCooldown += Time.deltaTime;
        }
        else
        {
            chasePlayer();
        }
        
    }
    
    private void chasePlayer()
    {
        if (Vector3.Distance(playerPosition.position, transform.position) > minDistance)
        {
            Vector3 playerVector = new Vector3(playerPosition.position.x, playerPosition.position.y, 0);
            transform.Translate(playerVector * Time.deltaTime * chaseSpeed);
        }
    }
}
