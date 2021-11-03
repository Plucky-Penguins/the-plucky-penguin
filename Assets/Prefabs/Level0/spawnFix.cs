using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFix : MonoBehaviour
{
    public Rigidbody2D prb;
    public float x;
    public float y;

    // Update is called once per frame
    void Update()
    {
        prb.position = new Vector2(x, y);
        Destroy(gameObject);
        
    }
}
