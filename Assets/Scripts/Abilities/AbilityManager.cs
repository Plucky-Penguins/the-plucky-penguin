using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles ability slots and activations.
/// </summary>
public class AbilityManager : MonoBehaviour
{
    public List<AbilityInterface.IAbility> abilities = new List<AbilityInterface.IAbility>();

    public Dictionary<int, string> slotToKey = new Dictionary<int, string>();

    private void Start()
    {
        slotToKey.Add(0, "E");
        slotToKey.Add(1, "R");
        slotToKey.Add(2, "F");

        for(int i = 0; i < 3; i++)
        {
            abilities.Add(null);
        }
            
        // example of how to add abilities
        abilities[0] = GetComponent<BombAbility>();
        abilities[1] = GetComponent<ShieldAbility>();
        abilities[2] = GetComponent<BurstAbility>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && abilities[0] != null) // activate first ability slot
        {
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
}
