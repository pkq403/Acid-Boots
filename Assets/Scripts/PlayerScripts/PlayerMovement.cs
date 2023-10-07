using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float gravityScale = 2;
    private Vector3 scaleVector = new Vector3(3, 3, 1);

    [SerializeField] public float moveSpeed = 5;
    [SerializeField] public float jumpSpeed = 10;
    [SerializeField] public float[] walljumpSpeed = {3, 7};
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private float wallJumpCooldown;
    private float horizontalInput;

    // Executes each time the scene is loaded
    private void Awake() 
    {
        // References for rigidbody and animator
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        // Horizontal Movement
        
        
        // Flip Sprite
        if (horizontalInput > 0.01f) transform.localScale = scaleVector;
        else if (horizontalInput < -0.01f) transform.localScale = new Vector3(-scaleVector.x, 3, 1);
        

        // Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
                anim.SetBool("onwall", true);
            }
            else
            {
                body.gravityScale = gravityScale;
                anim.SetBool("onwall", false);
            }
            // Si presionas el boton, salta
            if (Input.GetButtonDown("Jump"))
                Jump();

            
        }
        else wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            // Jumping
            body.velocity = Vector2.up * jumpSpeed;
            anim.SetTrigger("jump");
        }
        else if (onWall())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * (walljumpSpeed[0] + 4), 0);
                transform.localScale = new Vector2(-Mathf.Sign(transform.localScale.x) * scaleVector.x, transform.localScale.y);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * walljumpSpeed[0], walljumpSpeed[1]);
            anim.SetTrigger("walljump");
            wallJumpCooldown = 0;
        }


    }
    
    // Method that checks by a raycast if playes is in the ground
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    // Method that checks by a raycast if playes is in the wall
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
