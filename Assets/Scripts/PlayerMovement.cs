using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    public LayerMask groundLayers;
    public Animator animator;

    [Header("Movement")]
    public float movementSpeed;
    float movementX = -1;

    [Header("Jump")]
    // hangcounter and hangtime gives an extra timing window where the player can jump shortly after leaving a platform
    public float hangtime;    // max hangtime
    float hangcounter = 0;    // current hangtime

    [Space(10)]
    public float jumpBufferTime;        // min amount of time where player can jump after hangtime
    public float jumpForce = 25f;
    
    float lastJumpTime;    // time since last jump, stops player from getting an extra jump during hangtime
    bool isJumping;       // is the player in the middle of a jump

    public bool doubleJumpUnlocked = true;
    bool canDoubleJump = true;       // is the player able to double jump

    [Header("Dash")]
    public bool dashUnlocked = true;
    public ParticleSystem dashParticles;   // the dash particles
    public float dashDist = 15f;           // distance of the dash
    public float dashCooldown = 0.5f;      // dash cooldown

    float currentDashCooldown = 0;         // what the current cooldown on dash is
    bool dashOnCooldown = false;           // is the dash on cooldown
    bool isDashing = false;                // is player in the middle of a dash

    [Header("Animation")]
    [HideInInspector]
    public bool facingRight = true;


    void Update()
    {
        // get horizontal input
        movementX = Input.GetAxisRaw("Horizontal");

        // adjust facing direction
        spriteDirection();

        // handle cooldowns
        if (dashOnCooldown)
        {
            currentDashCooldown += Time.deltaTime;
            if (currentDashCooldown > dashCooldown)
            {
                dashOnCooldown = false;
                currentDashCooldown = 0;
            }
        }

        #region Jumping
        // if grounded, reset hangtime
        if (Physics2D.OverlapBox(rb.position, new Vector2(1.5f, 0.5f), 0, groundLayers))
        {
            hangcounter = hangtime;
        }

        // if player is falling, player is not jumping
        if (rb.velocity.y < 0)
        {
            isJumping = false;
        }

        if (Input.GetButtonDown("Jump")) {
            JumpButtonDown();
        }

        if (hangcounter > 0)
        {
            canDoubleJump = true;
            GetComponent<Renderer>().material.color = Color.white;
        }

        // jump checks
        if (hangcounter > 0 && lastJumpTime > 0 && !isJumping)
        { 
            Jump(false);
        } else if(Input.GetButtonDown("Jump") && canDoubleJump && doubleJumpUnlocked)
        {
            canDoubleJump = false;
            Jump(true);
        }

        // stop ascending when jump is released
        // allows for short hops
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }
        #endregion

        #region Dashing
        // move dash particles to player
        dashParticles.transform.position = new Vector2(rb.position.x, rb.position.y + 1);

        // when pressing dash key
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUnlocked)
        {
            if (!dashOnCooldown)
            {
                Dash();
            }
        }
        #endregion

        // countdown the timers
        hangcounter -= Time.deltaTime;
        lastJumpTime -= Time.deltaTime;
    }

    void Jump(bool djump)
    {
        if (djump)
        {
            GetComponent<Renderer>().material.color = new Color(1, 1.2f, 1);
            Vector2 movement = new Vector2(rb.velocity.x, jumpForce/2);
            rb.velocity = movement;
        } else
        {
            GetComponent<Renderer>().material.color = new Color(1, 1.2f, 1);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    void JumpButtonDown()
    {
        lastJumpTime = jumpBufferTime;
    }

    void Dash()
    {
        dashOnCooldown = true;
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
