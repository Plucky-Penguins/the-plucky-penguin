using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAbility : MonoBehaviour
{
    public Rigidbody2D projectile;
    public float speed = 5f;
    public float cooldown = 0.5f;

    private float currentCooldown;

    // Start is called before the first frame update
    void Start()
    {
        currentCooldown = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E) && currentCooldown >= cooldown)
        {
            currentCooldown = 0;
            Rigidbody2D p = Instantiate(projectile, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);

            if (GetComponent<PlayerMovement>().facingRight)
            {
                p.velocity = new Vector2(speed,0);
            } else
            {
                p.velocity = new Vector2(-speed, 0);
            }
        }
    }
}
