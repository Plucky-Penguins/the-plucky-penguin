using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // check object by tag
        if (collision.gameObject.tag == "projectileDestroy")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "killable")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<EnemyAI>().takeDamage(damage, false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
