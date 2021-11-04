using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAbility : MonoBehaviour, AbilityInterface.IAbility
{
    public GameObject shield;
    public float cooldown = 1;

    [HideInInspector]
    public float currentCooldown;
    

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
            GetComponent<PlayerCombat>().iFrames(1000, false);
            Instantiate(shield, new Vector3(transform.position.x, transform.position.y + 1, -1), transform.rotation);

            StartCoroutine(expireIndicator());
        } 
    }

    public IEnumerator expireIndicator()
    {
        yield return new WaitForSeconds(1);
        GetComponent<PlayerCombat>().iFrames(200, true);
    }
}
