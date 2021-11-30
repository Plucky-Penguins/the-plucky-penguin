using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveStore : MonoBehaviour
{
    public void deselectAbility()
    {
        BuyAbility.selectedAbility = null;
    }

    public void Leave()
    {
        string nextScene = "";
        string[] listOfSceneNames = { "TutorialLevel_Scene", "Level1_Scene", "Level2_Scene", "Level3_Scene", "Boss" };

        // check if this gets set
        //print(Igloo.currentLevel.ToString());

        for (int i = 0; i < listOfSceneNames.Length - 1; i++) {
            if (Igloo.currentLevel == listOfSceneNames[i]) {
                nextScene = listOfSceneNames[i + 1];
            }
        }

        BuyAbility.buyAbility();
        SceneManager.LoadScene(nextScene);
    }
    
    public void LeaveEmpty()
    {
        string nextScene = "";
        string[] listOfSceneNames = { "TutorialLevel_Scene", "Level1_Scene", "Level2_Scene", "Level3_Scene", "Boss" };

        // check if this gets set
        print(Igloo.currentLevel.ToString());

        for (int i = 0; i < listOfSceneNames.Length - 1; i++) {
            if (Igloo.currentLevel == listOfSceneNames[i]) {
                nextScene = listOfSceneNames[i + 1];
            }
        }

        SceneManager.LoadScene(nextScene);
    }
}
