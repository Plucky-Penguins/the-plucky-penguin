using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fish_Handler : MonoBehaviour
{
    public TextMeshProUGUI collection;
    int level_fish;
    // total_fish should be carried across levels
    int total_fish;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("collectible")) {
            int fish_value = other.gameObject.GetComponent<Fish>().fish_value;
            collectFish(fish_value);
            Destroy(other.gameObject);
        }

    }

    // TODO: Update 66 to proper count of fish
    public void collectFish(int fish_value) {
        level_fish += fish_value;
        total_fish += fish_value;
        collection.text = "" + level_fish.ToString() + "/66";
    }

    /**
     * Resets the current level's displayed fish score.
     * Generally for use when you would like to move to the next level.
     */
    public void resetLevelFishScore() {
        level_fish = 0;
    }

    /**
     * This should be called in the shop to display the fish. 
     * Total fish collected from all levels should be displayed.
     */
    public int getAllFish() {
        return total_fish;
    }

    /**
     * Needed when using fish to buy abilities.
     * 
     */
    public void useFish(int fishCost) {
        total_fish -= fishCost;
    }

}
