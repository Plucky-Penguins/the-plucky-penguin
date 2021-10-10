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
        if (player.transform.position.x > enemy.transform.position.x)
        {
            //move right
            rb.velocity = new Vector2(speed, rb.velocity.y);
            enemy.transform.localScale = new Vector3(Mathf.Abs(enemy.transform.localScale.x)*-1, enemy.transform.localScale.y, 1);
            facingRight = true;

        } else
        {
            //move left
            rb.velocity = new Vector2(speed*-1, rb.velocity.y);
            enemy.transform.localScale = new Vector3(Mathf.Abs(enemy.transform.localScale.x), Mathf.Abs(enemy.transform.localScale.y), 1);
            facingRight = false;
        }
    }

    void OnCollisionEnter2D(Collision2D targetObj)
    {
        if (targetObj.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().takeDamage(1);
        }
    }

    void takeDamage(int damage_delt)
    {
        health -= damage_delt;
    }
    
}
