using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed;
    public Rigidbody2D rb;
    public float jumpForce = 20f;
    public LayerMask groundLayers;
    public Animator animator;

    float movementX = -1;

    // double jump
    public bool doubleJump;
    bool canDoubleJump = true;

    // normal jump
    bool isGrounded;
    bool canJump = true;

    // hangcounter and hangtime gives an extra timing window where the player can jump shortly after leaving a platform
    float hangtime = 0.2f;
    float hangcounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get horizontal input
        movementX = Input.GetAxisRaw("Horizontal");

        // if touching groundlayers
        isGrounded = Physics2D.OverlapCircle(rb.position, 0.5f, groundLayers);

        // grounded when touching the ground, or during the hangtime window
        if (isGrounded)
        {
            // stop player from getting in an extra jump during the hangtime window
            if (hangcounter <= (hangtime-0.1) || rb.velocity.y == 0)
            {
                canJump = true;
                canDoubleJump = true;
            }
            hangcounter = hangtime;
        } else
        {
            hangcounter -= Time.deltaTime;
        }

        // when pressing jump key
        if (Input.GetButtonDown("Jump"))
        {
            // normal jump case
            if (hangcounter > 0 && canJump)
            {
                canJump = false;
                Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
                rb.velocity = movement;
            }
            // double jump case seperate
            else if (doubleJump && canDoubleJump)
            {
                canDoubleJump = false;
                Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
                rb.velocity = movement;
            }

        }

        // stop ascending when jump is released
        // allows for short hops
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }

        // colour the player to indicate if they are still able to jump
        if (doubleJump)
        {
            if (!canDoubleJump && !canJump && hangcounter <= 0)
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.white;
            }
        } else
        {
            if (!canJump && hangcounter <= 0)
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.white;
            }
        }
        
        // flip sprite to moving direction
        if (movementX > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            animator.SetBool("Walk", true);

        } else if (movementX < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            animator.SetBool("Walk", true);

        } else
        {
            animator.SetBool("Walk", false);
        }
    }

    private void FixedUpdate()
    {
        // movement
        Vector2 movement = new Vector2(movementX * movementSpeed, rb.velocity.y);
        rb.velocity = movement;
    }
}
