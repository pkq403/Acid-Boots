using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    private Animator anim;
    private SpriteRenderer spr;

    private bool trigger; // it gets triggered
    private bool active; // when it blows fire and can hurt the player
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (!trigger)
            {
                StartCoroutine(ActivateFiretrap());
            }
            if (active)
            {
                col.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private IEnumerator ActivateFiretrap()
    {
        trigger = true;
        spr.color = Color.red;
        yield return new WaitForSeconds(activationDelay);

        spr.color = Color.white;
        active = true;
        anim.SetBool("activated", true);
        yield return new WaitForSeconds(activeTime);

        anim.SetBool("activated", false);
        trigger = false;
        active = false;
    }
}
