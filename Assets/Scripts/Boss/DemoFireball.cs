using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoFireball : MonoBehaviour, EnemyInterface.IEnemy
{
    public int damage = 1;
    public Sprite friendlyVersion;
    private bool backfire = false;

    private void Start()
    {
        GetComponent<Rigidbody2D>().constraints =
            RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY |
            RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check object by tag
        if (collision.gameObject.tag == "projectileDestroy")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerCombat>().takeDamage(1);
        }
    }

    public void takeDamage(int damage_dealt, bool doesKnockback = true)
    {
        if (!backfire)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            backfire = true;
            GetComponent<SpriteRenderer>().sprite = friendlyVersion;
            GetComponent<Renderer>().material.color = new Color(0, 255, 255, 1);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-15, 0);
        }
        
    }

    public void stun(float duration = 2)
    {
        //throw new System.NotImplementedException();
    }

    public void yeet()
    {
        //throw new System.NotImplementedException();
    }

    public Vector2 getPosition()
    {
        return this.transform.position;
    }
}
