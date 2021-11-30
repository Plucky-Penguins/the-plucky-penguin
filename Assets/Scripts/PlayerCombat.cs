using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    private float immunityTimer = 0;
    private bool immunity = false;

    // removed health variable and placed in PlayerHealth script
    // refer to player health with
    // GameObject.Find("Player").GetComponent<PlayerHealth>().health

    public LayerMask enemies;

    [Header("Slap")]
    private float cooldown = 0.35f;
    private float currentCooldown;
    [HideInInspector]
    public bool isSlapping = false;
    //public float slapRange;
    public int slapDamage = 1;
    private bool immunityFlashing = false;

    void Awake()
    {
        currentCooldown = cooldown;
        
    }

    // Update is called once per frame
    void Update()
    {
        // slap cooldown
        currentCooldown += Time.deltaTime;
        
        // attack
        if ((Input.GetKeyDown(KeyCode.Q) || (Gamepad.current != null && Gamepad.current.buttonWest.wasPressedThisFrame)) && currentCooldown >= cooldown && !GetComponent<PlayerMovement>().isDashing)
        {
            currentCooldown = 0;
            GetComponent<PlayerMovement>().animator.SetTrigger("Attack");
            StartCoroutine(Attack());
        }
        
        // Damage immunity Logic
        if (immunityTimer > 0)
        {
            //immunityTimer -= 1;
            immunityTimer -= (Time.deltaTime * 150);

            if ((int)immunityTimer % 2 == 0)
            {
                if (immunityFlashing) // Immunity flash is not always necessary
                {
                    GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, .2f);
                }
            }
            else
            {
                GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
            }
        }
        else if (immunityTimer <= 0)
        {
            GetComponent<Renderer>().material.color = Color.white;
            immunity = false;
            immunityFlashing = false;
            GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    IEnumerator Attack()
    {
        isSlapping = true;
        Collider2D[] enemiesToDamage;
        GetComponent<PlayerMovement>().animator.speed = 5;

        if (GetComponent<PlayerMovement>().facingRight)
        {
            enemiesToDamage = Physics2D.OverlapBoxAll(new Vector2(GetComponent<PlayerMovement>().rb.position.x + 1.6f, GetComponent<PlayerMovement>().rb.position.y + 1.2f),
            new Vector2(2.5f, 2.15f), 0, enemies);
        } else
        {
            enemiesToDamage = Physics2D.OverlapBoxAll(new Vector2(GetComponent<PlayerMovement>().rb.position.x - 1.6f, GetComponent<PlayerMovement>().rb.position.y + 1.2f),
            new Vector2(2.5f, 2.15f), 0, enemies);
        }

        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
               enemiesToDamage[i].GetComponent<EnemyInterface.IEnemy>().takeDamage(slapDamage);
            
        }
        GetComponent<PlayerMovement>().animator.speed = 1;
        yield return new WaitForSeconds(cooldown);
        isSlapping = false;
    }

    void OnDrawGizmosSelected()
    {
        // visualizer gizmos for attack range
        //Vector2 playerpos = GetComponent<PlayerMovement>().rb.position;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(GetComponent<PlayerMovement>().rb.position.x+1.6f, GetComponent<PlayerMovement>().rb.position.y+1.2f), 
            new Vector2(2.5f,2.15f));
        Gizmos.DrawWireCube(new Vector2(GetComponent<PlayerMovement>().rb.position.x-1.6f, GetComponent<PlayerMovement>().rb.position.y+1.2f), 
            new Vector2(2.5f,2.15f));
        //Gizmos.DrawWireSphere(new Vector2(GetComponent<PlayerMovement>().rb.position.x + 1, GetComponent<PlayerMovement>().rb.position.y + 1.5f), slapRange);
       // Gizmos.DrawWireSphere(new Vector2(GetComponent<PlayerMovement>().rb.position.x - 1, GetComponent<PlayerMovement>().rb.position.y + 1.5f), slapRange);
    }

    // damage handler
    public void takeDamage(int damage_taken)
    {
        if (!immunity)
        {
            GetComponent<PlayerHealth>().health -= damage_taken;
            if (GetComponent<PlayerHealth>().health <= 0)
            {
                //die
                if (SceneManager.GetActiveScene().name == "Boss")
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                } else
                {
                    GetComponent<PlayerMovement>().Respawn();
                    GetComponent<PlayerHealth>().health = 3;
                }
                
            }

            iFrames(200, true);
        }
    }

    // General immunity handler
    public void iFrames(int duration = 300, bool flashing = false)
    {
        immunity = true;
        immunityTimer = duration;
        immunityFlashing = flashing;
    }

    // collision with enemies
    void OnCollisionEnter2D(Collision2D collision)
    {
        // get all enemies in Enemies object
        if (LayerMask.LayerToName(collision.gameObject.layer) == "enemy")
        {
            // object from enemy component
            var enemy = collision.gameObject.GetComponent<EnemyInterface.IEnemy>();
            // object from player
            PlayerMovement player = GetComponent<PlayerMovement>();

            // head bounce check
            if (player.rb.position.y - 0.4 > enemy.getPosition().y)
            {
                iFrames(10);
                enemy.stun(2f);
                player.yeet();
                player.refresh();
            } 
            else
            {
                collision.gameObject.GetComponent<EnemyInterface.IEnemy>().yeet();
                takeDamage(1);
            }

            
        }
    }
}
