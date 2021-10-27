using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstAbility : MonoBehaviour
{
    public GameObject burst;
    public float cooldown = 1;

    private float currentCooldown;
    

    // Start is called before the first frame update
    void Start()
    {
        currentCooldown = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown += Time.deltaTime;
        

        if (Input.GetKeyDown(KeyCode.R) && currentCooldown >= cooldown)
        {
            currentCooldown = 0;
            Instantiate(burst, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
        }
    }
}
