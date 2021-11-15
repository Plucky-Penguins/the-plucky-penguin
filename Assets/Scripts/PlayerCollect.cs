using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    public TextMeshProUGUI collection;
    public int TotalFishCount;
    int level_score;

    // total_score should be carried across levels
    int total_score;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("collectible")) {
            int fish_value = other.gameObject.GetComponent<Fish>().fish_value;
            collectFish(fish_value);
            Destroy(other.gameObject);
        }

    }

    public void collectFish(int fish_value) {
        level_score += fish_value;
        /** TODO: /66 needs to be turned into a variable 
         * changed per level
         */
        collection.text = "" + level_score.ToString() + "/" + TotalFishCount;
    }

}
