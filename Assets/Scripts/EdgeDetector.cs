using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When the circle collider exits
    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Edge Nearby!");

    }

    // When the circle collider exits
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Eeeee");

    }
}
