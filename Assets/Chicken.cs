using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour, EnemyInterface.IEnemy
{
    private GameObject player;
    private bool facingRight = true;

    public float movespeed = 4;
    public float maxX;
    public float minX;

    private int health = 3;
    private bool cannotMove = false;
    private int curStunDuration = 0; // This is only used for managing the stunMe coroutine

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!cannotMove)
        {
            if (transform.position.x >= maxX)
            {
                facingRight = false;
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            } else if (transform.position.x <= minX)
            {
                facingRight = true;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            }

            if (facingRight)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(movespeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-movespeed, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        
    }

    public void takeDamage(int damage_dealt, bool doesKnockback = true)
    {
        health -= damage_dealt;
        GetComponent<Renderer>().material.color = Color.red;

        if (doesKnockback)
        {
            StartCoroutine(knockback(50));
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

    public void yeet()
    {
        StartCoroutine(knockback(18));
    }

    public Vector2 getPosition()
    {
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
