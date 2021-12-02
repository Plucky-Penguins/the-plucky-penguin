using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public TMPro.TextMeshProUGUI winText;
    public Text scoreText, highscoreText;


    private bossPhase currentPhase;
    public float newPhaseTimer;
    private float currentPhaseTimer = 0;
    private int healthPoints = 25;
    private bool dead = false;

    public enum bossPhase {
        smacking,
        shooting
    }

    // Start is called before the first frame update
    void Start()
    {
        currentPhase = bossPhase.smacking;
        bodySpawn = body.transform.position;
        body.transform.position = new Vector2(body.transform.position.x, body.transform.position.y - 50);
        bodyRb.velocity = new Vector2(0, 12);
        winText.enabled = false;
        scoreText.enabled = false;
        highscoreText.enabled = false;
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

            if (currentPhase == bossPhase.smacking && !dead)
            {
                if (!smacking) { StartCoroutine(headSmack()); }
                if (!following) { StartCoroutine(followPlayer()); }
            }
            
            if (currentPhase == bossPhase.shooting && !dead)
            {
                bodyRb.velocity = new Vector2(0,0);
                StartCoroutine(shoot());
            }

            if (healthPoints <= 15 && !dead)
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
            
            yield return new WaitForSeconds(30);
            if (Random.Range(0,2) == 1)
            {
                Instantiate(egg, new Vector2(-29, 38), transform.rotation);
            } else
            {
                Instantiate(heart, new Vector2(-29, 38), transform.rotation);
                Instantiate(egg, new Vector2(25, 38), transform.rotation);
            }
            Instantiate(heart, new Vector2(-2, 38), transform.rotation);
                
            spawning = false;
        }
        
    }

    public void swapPhases()
    {
        if (!dead)
        {
            // get new phase
            bossPhase newPhase = (bossPhase)Random.Range(0, System.Enum.GetValues(typeof(bossPhase)).Length);
            if (newPhase != currentPhase)
            {
                roar(0.5f);
                currentPhase = newPhase;
            }
        }
        
    }

    public IEnumerator shoot()
    {
        print("shooting");
        if (!shooting)
        {
            shooting = true;
            
            yield return new WaitForSeconds(Random.Range(1f, 2.5f));
            if (!dead)
            {
                Instantiate(projectile, new Vector2(transform.position.x, transform.position.y + 12), transform.rotation);
                shooting = false;
            }
        }
    }

    public IEnumerator headSmack()
    {
        print("smacking");
        if (currentPhase == bossPhase.smacking)
        {
            AudioController.aCtrl.playBossAttack();
            GetComponent<Animator>().SetTrigger("attack");
        }
             
        smacking = true;
        yield return new WaitForSeconds(Random.Range(2f,3.5f));
        
        if (currentPhase == bossPhase.smacking && !dead) 
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
        AudioController.aCtrl.playBossScreenShake();
        cam.GetComponent<CameraClamp>().shakeDuration = dur;
    }

    public IEnumerator facePlayer()
    {
        if (!turning)
        {
            turning = true;
            yield return new WaitForSecondsRealtime(2f);
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


            if (player.transform.position.x < (transform.position.x - followRangeX) && transform.position.x > -42 && !dead)
            {
                print("moving left");
                bodyRb.velocity = new Vector2(-movespeed, bodyRb.velocity.y);
            }
            else if (player.transform.position.x > (transform.position.x + followRangeX) && transform.position.x < 32 && !dead)
            {
                print("moving right");
                bodyRb.velocity = new Vector2(movespeed, bodyRb.velocity.y);
            } else if (!dead)
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
        StartCoroutine(changeColor(Color.red));
        healthPoints -= damage_dealt;
        if (GameObject.Find("healthbar"))
        {
            GameObject.Find("healthbar").GetComponent<BossHealthBar>().updateHealth(healthPoints, 25);
        }
        

        

        if (healthPoints <= 0)
        {
            Destroy(GameObject.Find("healthbar"));
            foreach (GameObject t in GameObject.FindGameObjectsWithTag("killable"))
            {
                if (t.name != "goose_head_0")
                {
                    Destroy(t);
                }
                
            }

            
            if (!dying)
            {
                dead = true;
                StartCoroutine(die());
                StartCoroutine(restartGame());
            }
            
        }
    }

    public IEnumerator die()
    {
        print(currentPhase);
        GetComponent<SpriteRenderer>().sortingLayerName = "goosehead";
        dying = true;
        roar(2f);
        bodyRb.velocity = new Vector2(0, -8);
        yield return new WaitForSecondsRealtime(3f);
        updateScore();
        winText.enabled = true;
        scoreText.enabled = true;
        highscoreText.enabled = true;
    }

    public void updateScore()
    {
        GameObject eh = GameObject.Find("EventSystem");
        eh.GetComponent<HighScoreScript>().saveScore();
        scoreText.GetComponent<ScoreText>().showText();
        highscoreText.GetComponent<HighScoreText>().showText();
    }

    public IEnumerator restartGame()
    {
        print("waiting");
        yield return new WaitForSecondsRealtime(8f);
        print("done waiting");
        //SceneManager.LoadScene("Main_Menu");
        Application.Quit();
    }

    public IEnumerator changeColor(Color c)
    {
        GetComponent<Renderer>().material.color = c;
        yield return new WaitForSeconds(0.25f);
        GetComponent<Renderer>().material.color = Color.white;
    }

    public void yeet() {}

    
}
