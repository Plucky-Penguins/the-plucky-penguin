using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionBad : MonoBehaviour
{
    public int damage = 1;
    private float counter = 0;

    private Vector3 playerPos;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // check object by tag
        if (collision.gameObject.tag == "projectileDestroy")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerCombat>().takeDamage(1);
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (playerPos-transform.position).normalized * 10 * Time.deltaTime;
        counter += Time.deltaTime;
        if (counter >= 10)
        {
            Destroy(gameObject);
        }
    }
}
