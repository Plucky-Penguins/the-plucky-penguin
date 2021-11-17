using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!active && collision.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().respawnPoint = transform.position;
            active = true;
        }
    }
}
