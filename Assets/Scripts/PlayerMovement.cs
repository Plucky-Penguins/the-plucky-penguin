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
    float hangtime = 0.8f;
    float hangcounter = 0;

    bool isGrounded;

    bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get horizontal input
        movementX = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(rb.position, 0.5f, groundLayers);

        if (isGrounded)
        {
            if (hangcounter <= (hangtime-0.1))
            {
                canJump = true;
            }

            hangcounter = hangtime;
        } else
        {
            hangcounter -= Time.deltaTime;
        }


        if (Input.GetButtonDown("Jump") && hangcounter > 0 && canJump)
        {
            canJump = false;
            Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = movement;
        }

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

    /*
    public bool IsGrounded()
    {
        //Collider2D groundCheck = Physics2D.OverlapCircle(rb.position, 0.5f, groundLayers);
        Collider2D groundCheck = Physics2D.OverlapArea(new Vector2(rb.position.x-0.9f,rb.position.y-0.5f), new Vector2(rb.position.x+0.9f, rb.position.y), groundLayers);
        if (groundCheck != null)
        {
            return true;
        }
        return false;
    }
    */

    private void FixedUpdate()
    {

        Vector2 movement = new Vector2(movementX * movementSpeed, rb.velocity.y);
        rb.velocity = movement;
        
    }
}
