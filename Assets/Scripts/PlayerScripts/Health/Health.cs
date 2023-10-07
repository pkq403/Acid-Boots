using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Starting Health")]
    // health is float, because of the health visual(healthbar) component attribute fillAmount
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; } // Esto indica que se puede leer desde fuera pero no modificar
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iframesTime;
    [SerializeField] private int flashNumber;
    private  SpriteRenderer spriteRend;


    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();  
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("dead");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }

        }
    }
    public void Heal(float _healValue)
    {
        currentHealth = Mathf.Clamp(currentHealth + _healValue, 0, startingHealth);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }
    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < flashNumber; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iframesTime / (flashNumber / 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iframesTime / (flashNumber / 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

}
