using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles ability slots and activations.
/// </summary>
public class AbilityManager : MonoBehaviour
{

    private void Awake()
    {
        /*for (int ai = 0; ai < Controller.abilities.Count; ai++)
        {
            if ((ai + 1) < Controller.abilities.Count - 1)
            {
                if (abilities[ai + 1] == null || ai == 2)
                {
                    print("Start: " + abilities[ai].getName());
                    for (int i = 0; i < listOfAllAbilities.Count; i++)
                    {
                        if (abilities[ai].getName() == listOfAllAbilities[i].getName())
                        {
                            abilities[ai] = listOfAllAbilities[i];
                        }
                    }
                }
            }

        }*/

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Controller.abilities[0] != null) // activate first ability slot
        {
            print("Update: " + Controller.abilities[0].getName());
            GameObject.Find("Player").GetComponent<PlayerCombat>().abilities[0].activateAbility();
        }
        
        if (Input.GetKeyDown(KeyCode.R) && Controller.abilities[1] != null) // activate second ability slot
        {
            print("Update: " + Controller.abilities[1].getName());
            GameObject.Find("Player").GetComponent<PlayerCombat>().abilities[1].activateAbility();
        }
        
        if (Input.GetKeyDown(KeyCode.F) && Controller.abilities[2] != null) // activate third ability slot
        {
            print("Update: " + Controller.abilities[2].getName());
            GameObject.Find("Player").GetComponent<PlayerCombat>().abilities[2].activateAbility();
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
