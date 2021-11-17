using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionBad : MonoBehaviour, EnemyInterface.IEnemy
{
    public int damage = 1;
    private float counter = 0;

    private Vector3 playerPos;
    private bool backfire = false;
    public Sprite friendlyVersion;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 moveDirection = (playerPos - transform.position).normalized * 15;
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // check object by tag
        if (collision.gameObject.tag == "projectileDestroy")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player" && !backfire)
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerCombat>().takeDamage(1);
        }

        if (collision.gameObject.tag == "killable" && backfire)
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<EnemyInterface.IEnemy>().takeDamage(damage);
        }

    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter >= 10)
        {
            Destroy(gameObject);
        }
    }

    public void takeDamage(int damage_dealt, bool doesKnockback = true)
    {
        if (!backfire)
        {
            backfire = true;
            GetComponent<SpriteRenderer>().sprite = friendlyVersion;
            GetComponent<Renderer>().material.color = new Color(0, 255, 255, 1);
            GetComponent<Rigidbody2D>().velocity = -(GetComponent<Rigidbody2D>().velocity);
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
