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

    // dash
    public bool dash = true;
    bool canDash = true;

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
            hangcounter = hangtime; // hangcounter is full when on ground
        } else
        {
            hangcounter -= Time.deltaTime; // hangcounter counts down when off the ground
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

        // dash key
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Dashing");
        }

        // stop ascending when jump is released
        // allows for short hops
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }

        jumpColorIndicator();
        spriteDirection();
    }

    // flip sprite to moving direction
    void spriteDirection()
    {
        if (movementX > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            animator.SetBool("Walk", true);

        }
        else if (movementX < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            animator.SetBool("Walk", true);

        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    // colour the player to indicate if they are still able to jump
    void jumpColorIndicator()
    {
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
        }
        else
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
    }

    private void FixedUpdate()
    {
        // movement
        Vector2 movement = new Vector2(movementX * movementSpeed, rb.velocity.y);
        rb.velocity = movement;
    }
}
