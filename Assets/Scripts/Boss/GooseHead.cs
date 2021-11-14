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
    private bool smacking = false;


    private bool facingRight = false;

    private Vector2 bodySpawn;

    public GameObject cam;
    public GameObject burst;
    public GameObject projectile;
    private bossPhase currentPhase;
    public float newPhaseTimer;
    private float currentPhaseTimer = 0;

    public enum bossPhase {
        smacking,
        shooting
    }

    // Start is called before the first frame update
    void Start()
    {
        currentPhase = bossPhase.shooting;
        bodySpawn = body.transform.position;
        //body.transform.position = new Vector2(body.transform.position.x, body.transform.position.y - 50);
        //bodyRb.velocity = new Vector2(0, 12);
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(new Vector2(bodyRb.position.x + 15, bodyRb.position.y - 8), new Vector2(0.5f, 0.5f));
        //Gizmos.DrawWireCube(new Vector2(bodyRb.position.x - 15, bodyRb.position.y - 8), new Vector2(0.5f, 0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        // jump to body position
        transform.position = body.transform.position;

        //StartCoroutine(doIntro());
        intro = false;

        StartCoroutine(facePlayer());

        
        if (!intro)
        {
            currentPhaseTimer += Time.deltaTime;
            if (currentPhaseTimer >= newPhaseTimer)
            {
                currentPhaseTimer = 0;
                // get new phase
                currentPhase = (bossPhase)Random.Range(0, System.Enum.GetValues(typeof(bossPhase)).Length);
                Debug.Log("changing phase: " + currentPhase);
            }

            if (currentPhase == bossPhase.smacking)
            {
                Debug.Log("smacking");
                if (!smacking) { StartCoroutine(headSmack()); }
                StartCoroutine(followPlayer());
            }
            
            if (currentPhase == bossPhase.shooting)
            {
                bodyRb.velocity = new Vector2(0,0);
                Instantiate(projectile, new Vector2(transform.position.x, transform.position.y+12), transform.rotation);
            }
            
        }
        
    }

    public IEnumerator headSmack()
    {
        if (currentPhase == bossPhase.smacking)
        {
            GetComponent<Animator>().SetTrigger("attack");
        }
             
        smacking = true;
        yield return new WaitForSeconds(Random.Range(1f,2f));
        
        if (currentPhase == bossPhase.smacking)
        {
            if (facingRight)
            {
                Instantiate(burst, new Vector2(bodyRb.position.x + 15, bodyRb.position.y - 8), transform.rotation);

            }
            else
            {
                Instantiate(burst, new Vector2(bodyRb.position.x - 15, bodyRb.position.y - 8), transform.rotation);
            }
        }
        
        smacking = false;

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
                facingRight = true;
            }
            else
            {
                transform.localScale = new Vector3(3f, 3f, 3f);
                facingRight = false;
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
        StartCoroutine(changeColor(Color.red));
        
    }

    public IEnumerator changeColor(Color c)
    {
        GetComponent<Renderer>().material.color = c;
        yield return new WaitForSeconds(0.25f);
        GetComponent<Renderer>().material.color = Color.white;
    }

    public void yeet() {}

    
}
