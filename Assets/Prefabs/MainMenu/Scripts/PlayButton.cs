using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    /**
     * This should only be set when 
     * a player has completed a level.
     */
    public string recentlyCompletedLevel = "";
    public void doPlayCurrentLevel() {
        print("play");
        SceneManager.LoadScene("TutorialLevel_Scene");
    }
}
