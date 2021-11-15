using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    Rigidbody2D player;

    public Vector3 initialPosition;
    public float shakeDuration = 0;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
        player = GameObject.Find("Player").GetComponent<PlayerMovement>().rb;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * 0.5f;

            shakeDuration -= Time.deltaTime * 1;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = player.position;
            if (player.position.y < 1)
            {
                transform.position = new Vector3(player.position.x, 1, -10);
                initialPosition = transform.localPosition;
            }
        }

    }

    public void Respawn(Vector2 p)
    {
        transform.position = p;
    }
}
