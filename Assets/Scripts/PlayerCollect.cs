using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    public TextMeshProUGUI collection;
    int level_score;
    // total_score should be carried across levels
    int total_score;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("collectible")) {
            int coinValue = other.gameObject.GetComponent<Fish>().coinValue;
            ChangeScore(coinValue);
            Destroy(other.gameObject);
        }

    }

    public void ChangeScore(int coinValue) {
        level_score += coinValue;
        /** TODO: /66 needs to be turned into a variable 
         * changed per level
         */
        collection.text = "" + level_score.ToString() + "/66";
    }

}
