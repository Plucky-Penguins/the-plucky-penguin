using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject enemy;
    public float cooldown;
    public float range;

    private GameObject player;
    private float maxCooldown;
    // Start is called before the first frame update
    void Start()
    {
        maxCooldown = cooldown;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (System.Math.Abs(player.transform.position.x - transform.position.x) > range
            || System.Math.Abs(player.transform.position.y - transform.position.y) > range) {
            GetComponent<Renderer>().material.color = Color.red;
        } else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }

        cooldown -= Time.deltaTime;
        if (cooldown <= 0 
            && System.Math.Abs(player.transform.position.x - transform.position.x) <= range
            && System.Math.Abs(player.transform.position.y - transform.position.y) <= range)
        {
            cooldown = maxCooldown;
            Instantiate(enemy, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
        }
    }
}
