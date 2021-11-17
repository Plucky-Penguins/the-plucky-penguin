using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour, EnemyInterface.IEnemy
{
    private Rigidbody2D rb;
    private GameObject player;

    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public Vector2 getPosition()
    {
        //throw new System.NotImplementedException();
        return transform.position;
    }

    public void stun(float duration = 2)
    {
        //throw new System.NotImplementedException();
    }

    public void takeDamage(int damage_dealt, bool doesKnockback = true)
    {
        StartCoroutine(knockback(50));
    }

    public void yeet()
    {
        throw new System.NotImplementedException();
    }
    public IEnumerator knockback(float force)
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 knockbackDirection = rb.position - playerPosition;
        rb.AddForce(knockbackDirection.normalized * force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "outOfBounds")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "projectileDestroy")
        {
            Instantiate(enemy, new Vector2(transform.position.x, transform.position.y + 5), transform.rotation);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
