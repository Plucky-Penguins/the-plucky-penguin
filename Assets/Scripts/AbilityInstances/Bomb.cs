using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float destroyTimer;
    public GameObject burst;

    private float maxTimer;
    private Rigidbody2D player;
    private float redValue;

    void Start()
    {
        maxTimer = destroyTimer;
        player = GameObject.Find("Player").GetComponent<PlayerMovement>().rb;
        transform.position = new Vector2(player.position.x, player.position.y + 2.5f);
        redValue = 0;

        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        destroyTimer -= Time.deltaTime;
        redValue++;
        GetComponent<Renderer>().material.color = new Color(redValue/255, 0, 0, 1);

        if (destroyTimer  <= 0)
        {
            Instantiate(burst, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
            Destroy(gameObject);
        }
    }

}
