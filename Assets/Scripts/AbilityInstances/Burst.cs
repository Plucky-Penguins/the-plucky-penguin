using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burst : MonoBehaviour
{
    public float destroyTimer;
    public int damage;
    public int growFactor;
    public bool moveToPlayer;

    private float maxTimer;
    private Rigidbody2D player;

    void Start()
    {
        maxTimer = destroyTimer;
        player = GameObject.Find("Player").GetComponent<PlayerMovement>().rb;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // check object by tag

        if (collision.gameObject.tag == "killable")
        {
            collision.gameObject.GetComponent<EnemyInterface.IEnemy>().takeDamage(damage);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // grow then shrink
        if (destroyTimer <= (maxTimer/2))
        {
            transform.localScale += new Vector3(-1,-1,-1) * Time.deltaTime * growFactor;
        } else
        {
            transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
        }

        if (moveToPlayer)
        {
            transform.position = new Vector2(player.position.x, player.position.y);
        }

        destroyTimer -= Time.deltaTime;
        if (destroyTimer  <= 0)
        {
            Destroy(gameObject);
        }
    }
}
