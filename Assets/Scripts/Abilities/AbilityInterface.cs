using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInterface : MonoBehaviour
{
    public interface IAbility
    {
        /// <summary>
        /// Activates the ability.
        /// </summary>
        public void activateAbility();

        /// <summary>
        /// Returns the current cooldown of the ability. Abilities do not
        /// activate when the cooldown > 0
        /// </summary>
        /// <returns></returns>
        public float getCurrentCooldown();

        /// <summary>
        /// Returns the cost of the ability
        /// </summary>
        /// <returns></returns>
        public int getCost();
    }
}
