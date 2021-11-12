using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInterface : MonoBehaviour
{
    public interface IEnemy
    {
        public void takeDamage(int damage_dealt, bool doesKnockback = true);

        public void stun(float duration = 2f);

        public void yeet();

    }
}
