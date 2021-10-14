using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 1)
        {
            transform.position = new Vector3(transform.position.x, 1, -10);
        }
    }

    public void Respawn(Vector2 p)
    {
        transform.position = p;
    }
}
