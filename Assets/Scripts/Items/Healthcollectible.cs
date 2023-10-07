using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthcollectible : MonoBehaviour
{
    [SerializeField] private float healValue;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<Health>().Heal(1);
            gameObject.SetActive(false);
        }
    }

}
