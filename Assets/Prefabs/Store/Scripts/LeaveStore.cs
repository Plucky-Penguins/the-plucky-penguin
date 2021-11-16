using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveStore : MonoBehaviour
{
    // change this based on past level -- set it on igloo?
    [Header("Loading")]
    public string nextScene;
    public void Leave()
    {
        SceneManager.LoadScene(nextScene);
    }
}
