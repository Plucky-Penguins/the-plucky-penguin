using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstAbility : MonoBehaviour, AbilityInterface.IAbility
{
    public GameObject burst;
    public float cooldown = 1;

    [HideInInspector]
    public float currentCooldown;

    public int getCost()
    {
        return 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown < 0)
        {
            currentCooldown = 0;
        }
    }

    public float getCurrentCooldown()
    {
        return currentCooldown;
    }

    public void activateAbility()
    {
        if (currentCooldown <= 0)
        {
            currentCooldown = cooldown;
            Instantiate(burst, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation);
        } 
    }
}
