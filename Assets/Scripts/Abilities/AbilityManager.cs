using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles ability slots and activations.
/// </summary>
public class AbilityManager : MonoBehaviour
{
    public Dictionary<string, AbilityInterface.IAbility> abilitySwitcher = new Dictionary<string, AbilityInterface.IAbility>();

    private void Awake()
    {
        abilitySwitcher.Add("Speed Boost", GameObject.Find("Player").GetComponent<SpeedAbility>());
        abilitySwitcher.Add("Shield", GameObject.Find("Player").GetComponent<ShieldAbility>());
        abilitySwitcher.Add("Snowball", GameObject.Find("Player").GetComponent<ProjectileAbility>());
        abilitySwitcher.Add("Burst", GameObject.Find("Player").GetComponent<BurstAbility>());
        abilitySwitcher.Add("Bomb", GameObject.Find("Player").GetComponent<BombAbility>());
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && Controller.abilities[0] != null) // activate first ability slot
        {
            abilitySwitcher[Controller.abilities[0].getName()].activateAbility();
        }
        
        if (Input.GetKeyDown(KeyCode.R) && Controller.abilities[1] != null) // activate second ability slot
        {
            abilitySwitcher[Controller.abilities[1].getName()].activateAbility();
        }
        
        if (Input.GetKeyDown(KeyCode.F) && Controller.abilities[2] != null) // activate third ability slot
        {
            abilitySwitcher[Controller.abilities[2].getName()].activateAbility();
        }
    }

    /*public static void addIndexAbility(int index, string abilityName) {
        print("Adding: " + abilityName);
        string manageAbilityName = "";
        manageAbilityName = abilityName;
        for (int i = 0; i < listOfAllAbilities.Count; i++) {
            // print(listOfAllAbilities[i].getName());
            if (manageAbilityName == listOfAllAbilities[i].getName()) {
                abilities[index] = listOfAllAbilities[i];
            }
        }
    }*/
}
