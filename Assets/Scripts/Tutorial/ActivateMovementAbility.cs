using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMovementAbility : MonoBehaviour
{

    public bool activateWallJump;
    public bool activateDoubleJump;
    public bool activateDash;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (activateWallJump)
            {
                player.GetComponent<PlayerMovement>().wallJumpUnlocked = true;
            }
            
            if (activateDoubleJump)
            {
                player.GetComponent<PlayerMovement>().doubleJumpUnlocked = true;
            }
            
            if (activateDash)
            {
                player.GetComponent<PlayerMovement>().dashUnlocked = true;
            }

            Destroy(gameObject);
        }
    }
}
