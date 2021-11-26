using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public Text text;

    // Update is called once per frame
    public void showText()
    {
        text.text = "Your Time: " + Mathf.RoundToInt(HighScoreScript.score);
    }
}
