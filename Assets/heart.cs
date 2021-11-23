using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().health++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
