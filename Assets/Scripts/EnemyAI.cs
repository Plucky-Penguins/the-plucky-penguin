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
        if (Mathf.Abs(player.transform.position.x - enemy.transform.position.x) < sight_range)
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

    void OnCollisionEnter2D(Collision2D targetObj)
    {
        if (targetObj.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerCombat>().takeDamage(1);
        }
    }

    public void takeDamage(int damage_delt, bool doesKnockback = true)
    {
        health -= damage_delt;
        GetComponent<Renderer>().material.color = Color.yellow;
        
        if (doesKnockback)
        {
            StartCoroutine(knockback());
        }
        
        if (health <= 0)
        {
            Destroy(enemy);
        }
    }

    // knockback on player slappp
    IEnumerator knockback()
    {
        beingPushed = true;
        Vector2 playerPosition = player.transform.position;
        Vector2 knockbackDirection = rb.position - playerPosition;
        rb.AddForce(knockbackDirection.normalized * 50f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        beingPushed = false;
    }
    
}
