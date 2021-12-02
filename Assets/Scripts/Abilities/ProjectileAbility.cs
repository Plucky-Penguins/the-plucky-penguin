using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAbility : MonoBehaviour, AbilityInterface.IAbility
{
    public Rigidbody2D projectile;
    public float speed = 5f;
    public float cooldown = 0.5f;

    [HideInInspector]
    public float currentCooldown;
    public int getCost()
    {
        return 25;
    }
    public string getName()
    {
        return "Snowball";
    }

    public string getDescription()
    {
        return "Shoot a snowball in a straight line that damages enemies.";
    }

    // Start is called before the first frame update
    void Start()
    {
        currentCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown < 0)
        {
            currentCooldown = 0;
        }
    }

    public float getCurrentCooldown()
    {
        return currentCooldown;
    }

    public void activateAbility()
    {
        if (currentCooldown <= 0)
        {
            currentCooldown = cooldown;
            AudioController.aCtrl.playSnowballSound();
            Rigidbody2D p = Instantiate(projectile, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);

            if (GetComponent<PlayerMovement>().facingRight)
            {
                p.transform.localScale = new Vector3(1f, 1f, 1f);
                p.velocity = new Vector2(speed, 0);

            }
            else
            {
                p.transform.localScale = new Vector3(-1f, 1f, 1f);
                p.velocity = new Vector2(-speed, 0);
            }
        }
    }
}
