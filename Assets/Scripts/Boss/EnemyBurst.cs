using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBurst : MonoBehaviour
{
    public float destroyTimer;
    public int damage;
    public int growFactor;
    public float startDelay;

    private float maxTimer;

    void Start()
    {
        maxTimer = destroyTimer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check object by tag

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerCombat>().takeDamage(1);
        }

    }

    // Update is called once per frame
    void Update()
    {
        

        if (startDelay <= 0)
        {
            destroyTimer -= Time.deltaTime;

            // grow then shrink
            if (destroyTimer <= (maxTimer / 2))
            {
                transform.localScale += new Vector3(-1, -1, -1) * Time.deltaTime * growFactor;
            }
            else
            {
                transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
            }

        } else
        {
            startDelay -= Time.deltaTime;
        }
        
        if (destroyTimer  <= 0)
        {
            Destroy(gameObject);
        }
    }
}
