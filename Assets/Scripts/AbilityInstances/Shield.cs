using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float destroyTimer;

    private float maxTimer;
    private Rigidbody2D player;

    void Start()
    {
        maxTimer = destroyTimer;
        player = GameObject.Find("Player").GetComponent<PlayerMovement>().rb;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector2(player.position.x, player.position.y+1);

        destroyTimer -= Time.deltaTime;
        if (destroyTimer  <= 0)
        {
            Destroy(gameObject);
        }
    }
}
