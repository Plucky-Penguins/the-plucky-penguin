using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAbility : MonoBehaviour, AbilityInterface.IAbility
{
    public GameObject boots;
    public float cooldown = 1;

    [HideInInspector]
    public float currentCooldown;

    public int getCost()
    {
        return 20;
    }
    public string getName()
    {
        return "Speed Boost";
    }

    public string getDescription()
    {
        return "Increase your movement speed for a short duration.";
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
            AudioController.aCtrl.playSpeedSound();
            GetComponent<PlayerCombat>().iFrames(1000, false);
            Instantiate(boots, new Vector3(transform.position.x, transform.position.y + 1, -1), transform.rotation);
        } 
    }
}
