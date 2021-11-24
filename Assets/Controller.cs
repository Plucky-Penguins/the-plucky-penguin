using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Controller
{
    public static List<AbilityInterface.IAbility> abilities = new List<AbilityInterface.IAbility>();

    public static Dictionary<int, string> slotToKey = new Dictionary<int, string>();

    public static List<AbilityInterface.IAbility> listOfAllAbilities = new List<AbilityInterface.IAbility>();
    
}
