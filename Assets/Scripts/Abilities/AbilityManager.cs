using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles ability slots and activations.
/// </summary>
public class AbilityManager : MonoBehaviour
{
    public static List<AbilityInterface.IAbility> abilities = new List<AbilityInterface.IAbility>();

    public static Dictionary<int, string> slotToKey = new Dictionary<int, string>();

    public List<AbilityInterface.IAbility> listOfAllAbilities = new List<AbilityInterface.IAbility>();

    private void Start()
    {
        if (slotToKey.Count != 3) {
            slotToKey.Add(0, "E");
            slotToKey.Add(1, "R");
            slotToKey.Add(2, "F");
        }

        if (abilities.Count < 3)
        {
            for (int i = 0; i < 3; i++)
            {
                abilities.Add(null);
            }
        }

        if (listOfAllAbilities.Count < 5)
        {
            listOfAllAbilities.Add(GameObject.Find("Player").GetComponent<BombAbility>());
            listOfAllAbilities.Add(GameObject.Find("Player").GetComponent<ShieldAbility>());
            listOfAllAbilities.Add(GameObject.Find("Player").GetComponent<SpeedAbility>());
            listOfAllAbilities.Add(GameObject.Find("Player").GetComponent<ProjectileAbility>());
            listOfAllAbilities.Add(GameObject.Find("Player").GetComponent<BurstAbility>());
        }

        print(listOfAllAbilities.Count);
        print(listOfAllAbilities);
        
        // fails here
        print(listOfAllAbilities[2]);
        print(listOfAllAbilities[2].getName());

        if (abilities[0] != null) {
            print("Start: " + abilities[0].getName());
            for (int i = 0; i < listOfAllAbilities.Count; i++) {
                if (abilities[0].getName() == listOfAllAbilities[i].getName()) {
                    abilities[0] = listOfAllAbilities[i];
                } 
            }
        }

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && abilities[0] != null) // activate first ability slot
        {
            print("Update: " + abilities[0].getName());
            abilities[0].activateAbility();
        }
        
        if (Input.GetKeyDown(KeyCode.R) && abilities[1] != null) // activate second ability slot
        {
            abilities[1].activateAbility();
        }
        
        if (Input.GetKeyDown(KeyCode.F) && abilities[2] != null) // activate third ability slot
        {
            abilities[2].activateAbility();
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
