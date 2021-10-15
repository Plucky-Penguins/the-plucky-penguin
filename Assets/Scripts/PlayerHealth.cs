using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // health value
    public int health;
    // max health
    public int numHearts;

    // FOR UI
    public Image[] hearts;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++) {

            // Health is recovered past max
            if (health > numHearts) {
                health = numHearts;
            }

            // update UI health based on health value
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else {
                hearts[i].sprite = emptyHeart;
            }

            // display health (up to 10)
            if (i < numHearts)
            {
                hearts[i].enabled = true;
            }
            else {
                hearts[i].enabled = false;
            }
        }
    }
}
