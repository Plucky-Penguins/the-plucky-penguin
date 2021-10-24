using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    Rigidbody2D player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>().rb;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y < 1)
        {
            transform.position = new Vector3(player.position.x, 1, -10);
        }
    }

    public void Respawn(Vector2 p)
    {
        transform.position = p;
    }
}
