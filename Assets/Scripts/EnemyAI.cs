using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player, enemy;
    public bool player_close;
    public float sight_range, speed;
    public Rigidbody2D rb;
    private bool facingRight;
    public int health;
    private bool beingPushed = false;
    // Start is called before the first frame update
    void Start()
    {
        player_close = false;
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
        if (Mathf.Abs(player.transform.position.x - enemy.transform.position.x) < sight_range && Mathf.Abs(player.transform.position.y - enemy.transform.position.y) < sight_range / 2)
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
        if (!beingPushed)
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

    public void takeDamage(int damage_delt, bool doesKnockback = true)
    {
        health -= damage_delt;
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

    public void yeet() {
        StartCoroutine(knockback(18));
    }


    // knockback on player slappp
    public IEnumerator knockback(float force)
    {
        beingPushed = true;
        Vector2 playerPosition = player.transform.position;
        Vector2 knockbackDirection = rb.position - playerPosition;
        rb.AddForce(knockbackDirection.normalized * force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        beingPushed = false;
    }
    
}
