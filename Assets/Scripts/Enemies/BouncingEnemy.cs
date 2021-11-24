using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingEnemy : MonoBehaviour, EnemyInterface.IEnemy
{
    private GameObject player;
    private bool facingRight;

    
    public float sight_range, speed;
    public float jumpHeight = 15;

    public int health = 2;
    private bool cannotMove = false;
    private bool player_close = false;
    private int curStunDuration = 0; // This is only used for managing the stunMe coroutine

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        player_close = PlayerInRange(player);   
        if (player_close)
        {
            moveEnemy(player);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "ground")
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }
        else if (collision.gameObject.name == "outOfBounds")
        {
            Destroy(gameObject);
        }
    }

    private bool PlayerInRange(GameObject player)
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < sight_range && Mathf.Abs(player.transform.position.y - transform.position.y) < sight_range)
        {
            return true;
        }
        return false;
    }

    private void moveEnemy(GameObject player)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (!cannotMove)
        {
            if (player.transform.position.x > transform.position.x + 5)
            {
                //move right
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
                facingRight = true;

            }
            else if (player.transform.position.x < transform.position.x - 5)
            {
                //move left
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, Mathf.Abs(transform.localScale.y), 1);
                facingRight = false;
            }
        }
    }

    public void takeDamage(int damage_dealt, bool doesKnockback = true)
    {
        health -= damage_dealt;
        GetComponent<Renderer>().material.color = Color.yellow;
        
        if (doesKnockback)
        {
            StartCoroutine(knockback(25));
        }
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void stun(float duration = 2f)
    {
        curStunDuration += 1;
        StartCoroutine(stunMe(duration));
    }

    public void yeet() {
        StartCoroutine(knockback(18));
    }

    public Vector2 getPosition() {
        return new Vector2(transform.position.x, transform.position.y);
    }

    // stun status efect
    public IEnumerator stunMe(float duration)
    {
        cannotMove = true;
        GetComponent<Renderer>().material.color = Color.blue;
        yield return new WaitForSeconds(duration);

        // only release the stun if there are not more stuns waiting to happen
        if (curStunDuration <= 1)
        {
            GetComponent<Renderer>().material.color = Color.white;
            cannotMove = false;
        }
        curStunDuration -= 1;
    }

    // knockback on player slappp
    public IEnumerator knockback(float force)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        cannotMove = true;
        Vector2 playerPosition = player.transform.position;
        Vector2 knockbackDirection = rb.position - playerPosition;
        rb.AddForce(knockbackDirection.normalized * force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        cannotMove = false;
    }
    
}
