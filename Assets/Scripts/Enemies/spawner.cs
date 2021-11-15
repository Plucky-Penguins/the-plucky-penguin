using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject enemy;
    public float cooldown;

    private float maxCooldown;
    // Start is called before the first frame update
    void Start()
    {
        maxCooldown = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            cooldown = maxCooldown;
            Instantiate(enemy, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
        }
    }
}
