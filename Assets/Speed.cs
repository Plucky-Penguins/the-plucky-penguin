using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    public float destroyTimer;

    private float maxTimer;
    private Rigidbody2D player;
    private float playerSpeed;

    void Start()
    {
        playerSpeed = GameObject.Find("Player").GetComponent<PlayerMovement>().movementSpeed;
        GameObject.Find("Player").GetComponent<PlayerMovement>().movementSpeed *= 2;
        maxTimer = destroyTimer;
        player = GameObject.Find("Player").GetComponent<PlayerMovement>().rb;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector2(player.position.x, player.position.y + 1);

        destroyTimer -= Time.deltaTime;
        if (destroyTimer <= maxTimer/3)
        {
            GetComponent<Renderer>().material.color = new Color(255/2, 1, 1, 1);
            if (GameObject.Find("Player").GetComponent<PlayerMovement>().movementSpeed > playerSpeed)
            {
                GameObject.Find("Player").GetComponent<PlayerMovement>().movementSpeed -= 0.1f;
            }
        }

        if (destroyTimer <= 0)
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().movementSpeed = playerSpeed;
            Destroy(gameObject);
        }
    }
}
