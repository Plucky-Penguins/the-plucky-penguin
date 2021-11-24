using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerStarter : MonoBehaviour
{
    private void Awake()
    {
        if (Controller.slotToKey.Count != 3)
        {
            Controller.slotToKey.Add(0, "E");
            Controller.slotToKey.Add(1, "R");
            Controller.slotToKey.Add(2, "F");
        }

        if (Controller.abilities.Count < 3)
        {
            for (int i = 0; i < 3; i++)
            {
                Controller.abilities.Add(null);
            }
        }

        Controller.listOfAllAbilities.Add(GetComponent<BombAbility>());
        Controller.listOfAllAbilities.Add(GetComponent<ShieldAbility>());
        Controller.listOfAllAbilities.Add(GetComponent<ProjectileAbility>());
        Controller.listOfAllAbilities.Add(GetComponent<BurstAbility>());
        Controller.listOfAllAbilities.Add(GetComponent<SpeedAbility>());

    }
}
