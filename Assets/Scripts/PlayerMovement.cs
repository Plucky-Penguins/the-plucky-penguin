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
    float movementY = -1;
    bool facingRight = true;

    // double jump
    public bool doubleJump = true;
    bool canDoubleJump = true;

    // normal jump
    bool isGrounded;
    bool canJump = true;

    // hangcounter and hangtime gives an extra timing window where the player can jump shortly after leaving a platform
    float hangtime = 0.2f;
    float hangcounter = 0;

    // dash
    public bool dash = true;
    public ParticleSystem dashParticles;
    public float dashDist = 15f;
    bool isDashing = false;
    bool canDash = true;

    // Update is called once per frame
    void Update()
    {
        // get horizontal input
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");

        // adjust facing direction
        spriteDirection();

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
                GetComponent<Renderer>().material.color = Color.white;
            }
            hangcounter = hangtime; // hangcounter is full when on ground
        } else
        {
            hangcounter -= Time.deltaTime; // hangcounter counts down when off the ground
        }

        // when pressing jump key
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // hold down to drop fast
        if (movementY == -1 && !isGrounded)
        {
            Vector2 movement = new Vector2(rb.velocity.x, -10f);
            rb.velocity = movement;
        }

        // move dash particles to player
        dashParticles.transform.position = new Vector2(rb.position.x, rb.position.y + 1);

        // when pressing dash key
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            Dash();
            
        }

        // stop ascending when jump is released
        // allows for short hops
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }

        //spriteDirection();
    }

    void Jump()
    {
        // normal jump case
        if (hangcounter > 0 && canJump)
        {
            canJump = false;
            Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = movement;
            GetComponent<Renderer>().material.color = new Color(1, 1.2f, 1);
        }
        // double jump case separate
        else if (doubleJump && canDoubleJump)
        {
            canDoubleJump = false;
            Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = movement;
            GetComponent<Renderer>().material.color = new Color(1, 1.5f, 1);
        }
    }

    void Dash()
    {
        if (facingRight)
        {
            // dash right
            StartCoroutine(Dash(1f));

        }
        else
        {
            // dash left
            StartCoroutine(Dash(-1f));
        }
    }

    IEnumerator Dash(float dir)
    {
        isDashing = true;
        dashParticles.Play();
        animator.SetTrigger("Dash");

        // stop moving downwards
        rb.velocity = new Vector2(rb.velocity.x, 0f);

        // do the dash
        rb.AddForce(new Vector2(dashDist * dir, 0f), ForceMode2D.Impulse);

        // wait for dash completion
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
        dashParticles.Clear();
        dashParticles.Stop();
    }

    // flip sprite to moving direction
    void spriteDirection()
    {
        if (!isDashing)
        {
            // right
            if (movementX > 0f)
            {
                facingRight = true;
                transform.localScale = new Vector3(1f, 1f, 1f);
                animator.SetBool("Walk", true);

                // rotate particles to correct direction
                if (dashParticles.transform.rotation.eulerAngles == new Vector3(90, 180, 0))
                {
                    dashParticles.transform.eulerAngles = new Vector3(270, 0, 0);
                }
            }

            // left
            else if (movementX < 0f)
            {
                facingRight = false;
                transform.localScale = new Vector3(-1f, 1f, 1f);
                animator.SetBool("Walk", true);

                // rotate particles to correct direction
                if (dashParticles.transform.rotation.eulerAngles == new Vector3(270, 0, 0))
                {
                    dashParticles.transform.eulerAngles = new Vector3(90, 180, 0); ;
                }

            }
            else
            {
                animator.SetBool("Walk", false);
            }
        }

    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            // movement
            Vector2 movement = new Vector2(movementX * movementSpeed, rb.velocity.y);
            rb.velocity = movement;
        }
        
    }
}
