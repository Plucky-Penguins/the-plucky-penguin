using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardZone : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        other.transform.position = new Vector2(0f, -2f);
    }

}
