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
    private bool shooting = false;
    private bool spawning = false;
    private bool dying = false;

    private bool facingRight = false;

    private Vector2 bodySpawn;

    public GameObject cam;
    public GameObject burst;
    public GameObject projectile;
    public GameObject egg;
    public GameObject heart;


    private bossPhase currentPhase;
    public float newPhaseTimer;
    private float currentPhaseTimer = 0;
    private float healthPoints = 30;

    public enum bossPhase {
        smacking,
        shooting,
        nothing,
        dead
    }

    // Start is called before the first frame update
    void Start()
    {
        currentPhase = bossPhase.smacking;
        bodySpawn = body.transform.position;
        body.transform.position = new Vector2(body.transform.position.x, body.transform.position.y - 50);
        bodyRb.velocity = new Vector2(0, 12);
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

        StartCoroutine(doIntro());

        StartCoroutine(facePlayer());

        
        if (!intro)
        {
            currentPhaseTimer += Time.deltaTime;
            if (currentPhaseTimer >= newPhaseTimer)
            {
                currentPhaseTimer = 0;
                swapPhases();
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
                StartCoroutine(shoot());
            }

            if (healthPoints <= 20)
            {
                StartCoroutine(spawnEggs());
            }
            
        }
        
    }

    public IEnumerator spawnEggs()
    {
        if (!spawning)
        {
            spawning = true;

            roar(0.2f);
            // spawn eggs
            if (Random.Range(0, 2) == 1)
                Instantiate(egg, new Vector2(-29, 38), transform.rotation);

            if (Random.Range(0,3) == 1)
            {
                Instantiate(egg, new Vector2(-2, 38), transform.rotation);
            } else
            {
                Instantiate(heart, new Vector2(-2, 38), transform.rotation);
            }
            if (Random.Range(0, 2) == 1)
                Instantiate(egg, new Vector2(25, 38), transform.rotation);
            yield return new WaitForSeconds(10);
            spawning = false;
        }
        
    }

    public void swapPhases()
    {
        // get new phase
        bossPhase newPhase = (bossPhase)Random.Range(0, System.Enum.GetValues(typeof(bossPhase)).Length);
        if (newPhase != currentPhase)
        {
            roar(0.5f);
            currentPhase = newPhase;
        }
    }

    public IEnumerator shoot()
    {
        if (!shooting)
        {
            shooting = true;
            
            yield return new WaitForSeconds(Random.Range(1f, 2.5f));
            Instantiate(projectile, new Vector2(transform.position.x, transform.position.y + 12), transform.rotation);
            shooting = false;
        }
    }

    public IEnumerator headSmack()
    {
        if (currentPhase == bossPhase.smacking)
        {
            GetComponent<Animator>().SetTrigger("attack");
        }
             
        smacking = true;
        yield return new WaitForSeconds(Random.Range(2f,4f));
        
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
            yield return new WaitForSeconds(1f);

            if (player.transform.position.x < (transform.position.x - followRangeX) || transform.position.x > 45)
            {
                bodyRb.velocity = new Vector2(-movespeed, bodyRb.velocity.y);
            }
            else if (player.transform.position.x > (transform.position.x + followRangeX) || transform.position.x < -45)
            {
                bodyRb.velocity = new Vector2(+movespeed, bodyRb.velocity.y);
            } else
            {
                
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
        healthPoints -= damage_dealt;

        Debug.Log(healthPoints);
        if (healthPoints <= 0)
        {
            currentPhase = bossPhase.dead;
            StartCoroutine(die());
        }
    }

    public IEnumerator die()
    {
        if (!dying)
        {
            GetComponent<SpriteRenderer>().sortingLayerName = "goosehead";
            dying = true;
            roar(2f);
            bodyRb.velocity = new Vector2(0, -8);
            yield return new WaitForSeconds(3);
            Destroy(body);
            Destroy(gameObject);
        }
        
    }

    public IEnumerator changeColor(Color c)
    {
        GetComponent<Renderer>().material.color = c;
        yield return new WaitForSeconds(0.25f);
        GetComponent<Renderer>().material.color = Color.white;
    }

    public void yeet() {}

    
}