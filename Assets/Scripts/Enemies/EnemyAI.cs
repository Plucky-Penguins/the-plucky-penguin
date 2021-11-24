using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, EnemyInterface.IEnemy
{
    public GameObject player, enemy;
    public bool player_close;
    public float sight_range, speed;
    public Rigidbody2D rb;
    private bool facingRight;
    public int health;
    private bool cannotMove = false;
    private int curStunDuration = 0; // This is only used for managing the stunMe coroutine

    // Start is called before the first frame update
    void Start()
    {
        player_close = false;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        player_close = PlayerInRange(player, enemy);   
        if (player_close)
        {
            moveEnemy(player, enemy);
        }
    }

    private bool PlayerInRange(GameObject player, GameObject enemy)
    {
        if (Mathf.Abs(player.transform.position.x - enemy.transform.position.x) < sight_range && Mathf.Abs(player.transform.position.y - enemy.transform.position.y) < sight_range)
        {
            player_close = true;
        } else
        {
            player_close = false;
        }
        return player_close;
    }

    private void moveEnemy(GameObject player, GameObject enemy)
    {
        if (!cannotMove)
        {
            if (player.transform.position.x > enemy.transform.position.x)
            {
                //move right
                rb.velocity = new Vector2(speed, rb.velocity.y);
                enemy.transform.localScale = new Vector3(Mathf.Abs(enemy.transform.localScale.x) * -1, enemy.transform.localScale.y, 1);
                facingRight = true;

            }
            else
            {
                //move left
                rb.velocity = new Vector2(speed * -1, rb.velocity.y);
                enemy.transform.localScale = new Vector3(Mathf.Abs(enemy.transform.localScale.x), Mathf.Abs(enemy.transform.localScale.y), 1);
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
            StartCoroutine(knockback(50));
        }
        
        if (health <= 0)
        {
            Destroy(enemy);
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
        return new Vector2(rb.position.x, rb.position.y);
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
        cannotMove = true;
        Vector2 playerPosition = player.transform.position;
        Vector2 knockbackDirection = rb.position - playerPosition;
        rb.AddForce(knockbackDirection.normalized * force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        cannotMove = false;
    }
    
}
