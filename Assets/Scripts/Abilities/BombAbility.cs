using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAbility : MonoBehaviour, AbilityInterface.IAbility
{
    public GameObject bomb;
    public float cooldown = 3;

    [HideInInspector]
    public float currentCooldown;

    public int getCost()
    {
        return 30;
    }

    public string getName()
    {
        return "Bomb";
    }

    public string getDescription()
    {
        return "Throw a bomb that explodes after a certain amount of time.";
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
            Instantiate(bomb, new Vector2(transform.position.x, transform.position.y + 2), transform.rotation);
        } 
    }

    
}
