using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // damage for enemies
        Debug.Log("spiking!");
        if (LayerMask.LayerToName(collision.gameObject.layer) == "enemy")
        {
            collision.gameObject.GetComponent<EnemyInterface.IEnemy>().takeDamage(1);
            collision.gameObject.GetComponent<EnemyInterface.IEnemy>().yeet();
        }
        else if (collision.gameObject.name == "Player") {
            collision.gameObject.GetComponent<PlayerCombat>().takeDamage(1);
            collision.gameObject.GetComponent<PlayerMovement>().yeet();
            collision.gameObject.GetComponent<PlayerMovement>().refresh();
        }
        else {
            Debug.Log(collision.gameObject.name);
        }
    }
}
