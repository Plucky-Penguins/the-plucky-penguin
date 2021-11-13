using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bear : MonoBehaviour, EnemyInterface.IEnemy
{
    [Header("References")]
    public GameObject player;
    public GameObject bearObj;
    public Rigidbody2D rb;
    public LayerMask groundLayers;
    public Animator animator;

    [Header("Stats")]
    public float sight_range;
    public float speed;
    public int health;

    public bool player_close = false;
    private bool cannotMove = false;
    private int curStunDuration = 0; // This is only used for managing the stunMe coroutine
    private Directions walls;
    private bool facingRight = true;
    private int jumpCooldown = 100;
    private int playerToRight; // Set to 1 if player is to right, set to -1 if player is left
    private string lastJumpDir;
    float distToGround; 

    [HideInInspector]
    private float width;
    private float height;
    private enum Directions
    {
        Left,
        Right,
        None
    }

    // Start is called before the first frame update
    void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;

        // // AN ALTERNATIVE I TRIED
        // BoxCollider2D collider = GetComponent<BoxCollider2D>();
        // width = collider.bounds.extents.x * 2;
        // height = collider.bounds.extents.y * 2;

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(rb.position.x + width/2, rb.position.y), new Vector3(0.25f, height/2, 1));
        Gizmos.DrawWireCube(new Vector2(rb.position.x - width/2, rb.position.y), new Vector3(0.25f, height/2, 1));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector2(rb.position.x, rb.position.y - height / 2), new Vector3(width * 0.9f, 0.5f, 1));
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded() && jumpCooldown > 0)
        {
            // tick down jump cooldown
            jumpCooldown--;
        }

        findPlayerToRight();

        #region Wall Check
        // get horizontal walls
        if (Physics2D.OverlapBox(new Vector2(rb.position.x + width/2, rb.position.y), new Vector2(0.25f, height/2), 0, groundLayers)) // right side
        {
            walls = Directions.Right;
        }
        else if (Physics2D.OverlapBox(new Vector2(rb.position.x - width/2, rb.position.y), new Vector2(0.25f, height/2), 0, groundLayers)) // left side
        {
            walls = Directions.Left;
        }
        else
        {
            walls = Directions.None;
        }
        #endregion

        player_close = PlayerInRange(player, bearObj);
        if (player_close)
        {
            chasePlayer(bearObj);
        }
        else
        {
            // do normal walk cycle
            WalkAround(bearObj);
        }
    }

    private bool PlayerInRange(GameObject player, GameObject bearObj)
    {
        if (Mathf.Abs(player.transform.position.x - bearObj.transform.position.x) < sight_range && Mathf.Abs(player.transform.position.y - bearObj.transform.position.y) < sight_range / 2)
        {
            player_close = true;
        }
        else
        {
            player_close = false;
        }
        return player_close;
    }

    private void WalkAround(GameObject bearObj)
    {
        if (!cannotMove && isGrounded())
        {
            #region Wall Check
            // If there's a wall, turn around
            if (walls == Directions.Left)
            {
                if (isGrounded())
                {
                    facingRight = true;
                }
            }
            else if (walls == Directions.Right)
            {
                if (isGrounded())
                {
                    facingRight = false;
                }
            }
            #endregion

            if (facingRight)
            {
                //move right
                rb.velocity = new Vector2(speed, rb.velocity.y);
                bearObj.transform.localScale = new Vector3(Mathf.Abs(bearObj.transform.localScale.x), bearObj.transform.localScale.y, 1);
            }
            else
            {
                //move left
                rb.velocity = new Vector2(speed * -1, rb.velocity.y);
                bearObj.transform.localScale = new Vector3(Mathf.Abs(bearObj.transform.localScale.x) * -1, Mathf.Abs(bearObj.transform.localScale.y), 1);
            }
        }
    }

    private void chasePlayer(GameObject bearObj)
    {
        if (!cannotMove) {
            #region turn to player
            // Turn to face the player
            if (playerToRight == 1)
            {
                facingRight = true;
            } else if (playerToRight == -1)
            {
                facingRight = false;
            }
            // flip sprite
            bearObj.transform.localScale = new Vector3(Mathf.Abs(bearObj.transform.localScale.x) * playerToRight, bearObj.transform.localScale.y, 1);
            #endregion

            // If the bear is on the ground:
            if (isGrounded())
            {

                // Move towards the player
                rb.velocity = new Vector2(speed * playerToRight, rb.velocity.y);

                // Also Jump towards player
                if (jumpCooldown == 0)
                {
                    StartCoroutine(Jump());
                    jumpCooldown = 200;
                }
            }
            else if (playerToRight == 1 && lastJumpDir == "right") // if jumping towards the player, and the bear is still hasn't passed by them:
            {
                // move towards the player so that walls don't destroy all velocity
                Vector2 curVelocity = new Vector2(rb.velocity.x, rb.velocity.y);
                curVelocity.x += 0.1f;
                rb.velocity = curVelocity;
            }
            else if (playerToRight == -1 && lastJumpDir == "left") // if jumping towards the player, and the bear is still hasn't passed by them:
            {
                // move towards the player so that walls don't destroy all velocity
                Vector2 curVelocity = new Vector2(rb.velocity.x, rb.velocity.y);
                curVelocity.x -= 0.1f;
                rb.velocity = curVelocity;
            }
        }

    }

    public void takeDamage(int damage_dealt, bool doesKnockback = true)
    {
        health -= damage_dealt;
        GetComponent<Renderer>().material.color = Color.yellow;

        if (doesKnockback)
        {
            StartCoroutine(knockback(50));
        }

        if (health <= 0)
        {
            Destroy(bearObj);
        }
    }

    public void stun(float duration = 2f)
    {
        curStunDuration += 1;
        StartCoroutine(stunMe(duration));
    }

    public void yeet()
    {
        StartCoroutine(knockback(18));
    }

    public Vector2 getPosition() {
        return new Vector2(rb.position.x, rb.position.y);
    }

    // find if grounded or not
    private bool isGrounded()
    {
        // THIS DIDN'T WORK
        // Debug.Log(Physics.Raycast(transform.position, -Vector3.up, height + 0.2f));
        // return Physics.Raycast(transform.position, -Vector3.up, height + 0.2f);

        // THIS DIDN'T WORK
        // var result = Physics.CheckCapsule(
        //     GetComponent<BoxCollider2D>().bounds.center,
        //     new Vector3(GetComponent<BoxCollider2D>().bounds.center.x,
        //         GetComponent<BoxCollider2D>().bounds.min.y-0.1f,
        //         GetComponent<BoxCollider2D>().bounds.center.z),
        //     0.18f);

        // Debug.Log(result);
        // return result;

        if (Physics2D.OverlapBox(new Vector2(rb.position.x, rb.position.y - height / 2), new Vector2(width * 0.9f, 0.2f), 0, groundLayers))
        {
            Debug.Log($"grounded, {Physics2D.OverlapBox(new Vector2(rb.position.x, rb.position.y - height / 2), new Vector2(width * 0.9f, 0.2f), 0, groundLayers)}");
            return true;
        }
        else
        {
            Debug.Log("not grounded");
            return false;
        }
    }

    // sets value of playerToRight
    private void findPlayerToRight()
    {
        if (player.transform.position.x > bearObj.transform.position.x)
        {
            // Chase them right
            playerToRight = 1;
        }
        else
        {
            // Chase them left
            playerToRight = -1;
        }
    }

    // Don't move off the edge of a platform
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "platforms")
        {
            // Turn around
            facingRight = !facingRight;
        }
    }

    // stun status efect
    public IEnumerator stunMe(float duration)
    {
        cannotMove = true;
        animator.speed = 0;
        GetComponent<Renderer>().material.color = Color.blue;
        yield return new WaitForSeconds(duration);

        // only release the stun if there are not more stuns waiting to happen
        if (curStunDuration <= 1)
        {
            GetComponent<Renderer>().material.color = Color.white;
            cannotMove = false;
            animator.speed = 1;
        }
        curStunDuration -= 1;
    }

    // knockback on player slappp
    public IEnumerator knockback(float force)
    {
        cannotMove = true;
        Vector2 playerPosition = player.transform.position;
        Vector2 knockbackDirection = rb.position - playerPosition;
        rb.AddForce(knockbackDirection.normalized * force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        cannotMove = false;
    }

    IEnumerator Jump()
    {
        if (facingRight)
        {
            lastJumpDir = "right";
        } else { lastJumpDir = "left"; }

        // add jump force
        rb.AddForce(new Vector2(10f, 30f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
    }
}
