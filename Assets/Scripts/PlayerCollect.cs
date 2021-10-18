using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    public TextMeshProUGUI collection;
    int score;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("collectible")) {
            int coinValue = other.gameObject.GetComponent<Fish>().coinValue;
            ChangeScore(coinValue);
            Destroy(other.gameObject);
        }

    }

    public void ChangeScore(int coinValue) {
        score += coinValue;
        collection.text = "" + score.ToString();
    }

}
