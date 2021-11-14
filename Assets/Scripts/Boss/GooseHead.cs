using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseHead : MonoBehaviour, EnemyInterface.IEnemy
{
    public GameObject body;
    public Rigidbody2D bodyRb;
    public GameObject player;

    public float followRangeX;
    public float movespeed;

    private bool intro = true;

    private bool turning = false;
    private bool following = false;
    private Vector2 bodySpawn;

    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        bodySpawn = body.transform.position;
        body.transform.position = new Vector2(body.transform.position.x, body.transform.position.y - 50);
        bodyRb.velocity = new Vector2(0, 12);
    }

    // Update is called once per frame
    void Update()
    {
        // jump to body position
        transform.position = body.transform.position;

        StartCoroutine(doIntro());
        StartCoroutine(facePlayer());

        if (!intro)
        {
            StartCoroutine(followPlayer());
        }
        
    }

    public IEnumerator doIntro()
    {
        if (body.transform.position.y >= bodySpawn.y && intro)
        {
            bodyRb.velocity = new Vector2(0, 0);

            yield return new WaitForSeconds(0.5f);
            roar(0.8f);
            
            intro = false;
            GetComponent<SpriteRenderer>().sortingLayerName = "platforms";
        }
    }

    public void roar(float dur)
    {
        cam.GetComponent<CameraClamp>().shakeDuration = dur;
    }

    public IEnumerator facePlayer()
    {
        if (!turning)
        {
            turning = true;
            yield return new WaitForSecondsRealtime(1.5f);
            // face the player
            if (player.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-3f, 3f, 3f);
            }
            else
            {
                transform.localScale = new Vector3(3f, 3f, 3f);
            }
            turning = false;
        }
        
    }

    public IEnumerator followPlayer()
    {
        if (!following)
        {
            following = true;
            yield return new WaitForSeconds(1.5f);

            if (player.transform.position.x < (transform.position.x - followRangeX))
            {
                bodyRb.velocity = new Vector2(-movespeed, bodyRb.velocity.y);
            }
            else if (player.transform.position.x > (transform.position.x + followRangeX))
            {
                bodyRb.velocity = new Vector2(+movespeed, bodyRb.velocity.y);
            }
            else
            {
                bodyRb.velocity = new Vector2(0, bodyRb.velocity.y);
            }
            
            following = false;
        }
        
    }

    public Vector2 getPosition()
    {
        return transform.position;
    }

    public void stun(float duration = 2){}

    public void takeDamage(int damage_dealt, bool doesKnockback = true)
    {
        Debug.Log("boss takes damage!");
    }

    public void yeet() {}

    
}
