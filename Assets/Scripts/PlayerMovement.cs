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
    public ParticleSystem doubleJumpParticles;   // the double jump particles

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
    [HideInInspector]
    public bool isDashing = false;                // is player in the middle of a dash
    private bool canDash = true;

    [Header("Animation")]
    [HideInInspector]
    public bool facingRight = true;

    [Header("Wall Jump")]
    public bool wallJumpUnlocked = true;
    private Directions walls;
    private bool isWallJumping = false;

    [HideInInspector]
    public Vector2 respawnPoint;

    public float WallJumpTimer;
    public float WallJumpHorizontal;
    public float WallJumpVertical;
    private float minwalljumptimer = 0;

    private enum Directions
    { 
        Left,
        Right,
        None,
        Both
    }

    void Start()
    {
        respawnPoint = transform.position;
    }

    void OnDrawGizmosSelected()
    {
        // visualizer gizmos for wall detection

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector2(rb.position.x + 1, rb.position.y + 1), new Vector2(0.25f, 1.5f));
        Gizmos.DrawWireCube(new Vector2(rb.position.x - 1, rb.position.y + 1), new Vector2(0.25f, 1.5f));
    }

    void Update()
    {
        // get horizontal input
        movementX = Input.GetAxisRaw("Horizontal");

        // adjust facing direction
        spriteDirection();
        
        // get horizontal walls
        if (wallJumpUnlocked)
        {
            if (Physics2D.OverlapBox(new Vector2(rb.position.x + 1, rb.position.y + 1), new Vector2(0.25f, 1.5f), 0, groundLayers) && (Physics2D.OverlapBox(new Vector2(rb.position.x - 1, rb.position.y + 1), new Vector2(0.25f, 1.5f), 0, groundLayers)))
            {
                walls = Directions.Both;
            }
            else if (Physics2D.OverlapBox(new Vector2(rb.position.x + 1, rb.position.y + 1), new Vector2(0.25f, 1.5f), 0, groundLayers)) // right side
            {
                walls = Directions.Right;
                facingRight = false;
                canDash = true;
                canDoubleJump = true;
            }
            else if (Physics2D.OverlapBox(new Vector2(rb.position.x - 1, rb.position.y + 1), new Vector2(0.25f, 1.5f), 0, groundLayers)) // left side
            {
                walls = Directions.Left;
                facingRight = true;
                canDash = true;
                canDoubleJump = true;
            }
            else
            {
                walls = Directions.None;
                animator.SetBool("WallSlide", false);
            }
        }
        
        

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
        if (isGrounded())
        {
            hangcounter = hangtime;
            canDash = true;
        }
        
        // wall jump check
        if (walls == Directions.Left && wallJumpUnlocked && !isGrounded())
        {
            animator.SetBool("WallSlide", true);
            refresh();

            if(!isWallJumping)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            
            if (Input.GetButtonDown("Jump") && !isGrounded()) // jump off left wall, to the right
            {
                WallJump(1f);

                facingRight = true;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        } else if (walls == Directions.Right && wallJumpUnlocked && !isGrounded())
        {
            animator.SetBool("WallSlide", true);
            refresh();

            if(!isWallJumping)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            

            if (Input.GetButtonDown("Jump") && !isGrounded()) // jump off right wall, to the left
            {
                WallJump(-1f);

                facingRight = false;
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        // If the player is not next to a wall, or wall jump is not unlocked
        else
        {
            animator.SetBool("WallSlide", false);
        }

        // countdown the timers
        hangcounter -= Time.deltaTime;
        lastJumpTime -= Time.deltaTime;

        // if player is falling, player is not jumping
        if (rb.velocity.y < 0)
        {
            isJumping = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            JumpButtonDown();
        }

        if (hangcounter > 0)
        {
            canDoubleJump = true;
        }

        // jump checks
        if (hangcounter > 0 && lastJumpTime > 0 && !isJumping)
        {
            Jump(false);
        }
        else if (Input.GetButtonDown("Jump"))
        {
            if (canDoubleJump && doubleJumpUnlocked && !isGrounded()) {
                if ((wallJumpUnlocked && walls == Directions.None) || !wallJumpUnlocked)
                {
                    canDoubleJump = false;
                    Jump(true);
                }
            }
        }

        if (isWallJumping)
        {
            minwalljumptimer += Time.deltaTime;
            if (minwalljumptimer >= WallJumpTimer)
            {
                isWallJumping = false;
                minwalljumptimer = 0;
            }
        } else
        {
            minwalljumptimer = 0;
        }

        

        // stop ascending when jump is released
        // allows for short hops
        if (Input.GetButtonUp("Jump"))
        {
            //if (minwalljumptimer >= 0.3)
            //{
                isWallJumping = false;
                minwalljumptimer = 0;
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
                Debug.Log("cancel");
            //}
            
            if (rb.velocity.y > 1)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
            }
            
        }
        

        #endregion

        #region Dashing
        // move dash particles to player
        // dashParticles.transform.position = new Vector2(rb.position.x, rb.position.y + 1);

        // when pressing dash key
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUnlocked && !GetComponent<PlayerCombat>().isSlapping)
        {
            if (!dashOnCooldown && canDash)
            {
                canDash = false;
                Dash();
            }
        }
        #endregion
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "outOfBounds")
        {
            Respawn();
            GetComponent<PlayerCombat>().takeDamage(1);
        }

    }

    void Dash()
    {
        dashOnCooldown = true;
        if (facingRight)
        {
            // dash right
            StartCoroutine(Dash(2f));
        }
        else
        {
            // dash left
            StartCoroutine(Dash(-2f));
        }
    }

    void Respawn()
    {
        transform.position = respawnPoint;

        // reposition camera
        GameObject.FindWithTag("MainCamera").GetComponent<CameraClamp>().Respawn(respawnPoint);

    }

    void Jump(bool djump)
    {
        if (djump) // when double jumping
        {
            Vector2 movement = new Vector2(rb.velocity.x, jumpForce/2);
            rb.velocity = movement;
            doubleJumpParticles.Play();
            doubleJumpParticles.transform.position = new Vector2(rb.position.x, rb.position.y); // align the particles with the player

        } else // when normal jumping
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    void JumpButtonDown()
    {
        lastJumpTime = jumpBufferTime;
    }

    public void yeet(float force = 25)
    {
        StartCoroutine(knockback(force));
    }

    public void refresh(string ability = "all")
    {
        /// <summary> Refreshes dash/jump cooldowns </summary>
        /// This function exists so that other entities (like world pickups or abilities) can interact with cooldowns
        /// Pass it the name of abilities to refresh or leave it blank to refresh all

        // Dash
        if (ability.Contains("dash") || ability.Contains("all"))
        {
            canDash = true;
            dashOnCooldown = false;
        }

        // Double Jump
        if (ability.Contains("djump") || ability.Contains("all"))
        {
            canDoubleJump = true;
        }
    }

    // knock back the player (currently only knocks upward)
    public IEnumerator knockback(float force)
    {
        Vector2 knockbackDirection = new Vector2(0f, 1f);
        rb.AddForce(knockbackDirection.normalized * force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator Dash(float dir)
    {
        // Become immune during dash
        GetComponent<PlayerCombat>().iFrames(100);

        isDashing = true;

        // dash animation direction
        if (dir > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        } else if (dir < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        dashParticles.Play();
        animator.SetTrigger("Dash");

        // stop moving downwards
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        Physics2D.gravity = new Vector2(0f, 0f);

        // do the dash
        rb.AddForce(new Vector2(dashDist * dir, 0f), ForceMode2D.Impulse);

        // wait for dash completion
        yield return new WaitForSeconds(0.4f);
        Physics2D.gravity = new Vector2(0f, -15f);
        isDashing = false;
    }

    void WallJump(float dir)
    {
        isWallJumping = true;
        animator.SetBool("WallSlide", false);

        // vertical
        //rb.AddForce(new Vector2(20 * dir, 0), ForceMode2D.Impulse);

        // horizontal
        Vector2 movement = new Vector2(dir * WallJumpHorizontal, WallJumpVertical);
        rb.velocity = movement;
        //rb.AddForce(new Vector2(20 * dir, 20*5), ForceMode2D.Impulse);

        //yield return new WaitForSeconds(WallJumpTimer);
        //isWallJumping = false;
    }

    // find if grounded or not
    private bool isGrounded()
    {
        if (Physics2D.OverlapBox(rb.position, new Vector2(1.5f, 0.5f), 0, groundLayers))
        {
            return true;
        } else
        {
            return false;
        }
    }

    // flip sprite to moving direction
    void spriteDirection()
    {
        if (!isDashing && !GetComponent<PlayerCombat>().isSlapping && !isWallJumping)
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
        if (!isDashing && !isWallJumping)
        {
            // movement
            Vector2 movement = new Vector2(movementX * movementSpeed, rb.velocity.y);
            rb.velocity = movement;
        }
        
    }

    
}
