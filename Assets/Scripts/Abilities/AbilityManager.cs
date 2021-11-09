using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles ability slots and activations.
/// </summary>
public class AbilityManager : MonoBehaviour
{
    public AbilityInterface.IAbility abilitySlot1 = null;
    public AbilityInterface.IAbility abilitySlot2 = null;
    public AbilityInterface.IAbility abilitySlot3 = null;

    private void Start()
    {
        // example of how to add abilities
        //abilitySlot1 = GetComponent<BombAbility>();
        abilitySlot2 = GetComponent<ProjectileAbility>();
        //abilitySlot3 = GetComponent<ShieldAbility>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && abilitySlot1 != null) // activate first ability slot
        {
            abilitySlot1.activateAbility();
        }
        
        if (Input.GetKeyDown(KeyCode.R) && abilitySlot2 != null) // activate second ability slot
        {
            abilitySlot2.activateAbility();
        }
        
        if (Input.GetKeyDown(KeyCode.F) && abilitySlot3 != null) // activate third ability slot
        {
            abilitySlot3.activateAbility();
        }


    }
}
