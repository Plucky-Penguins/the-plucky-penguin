using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveStore : MonoBehaviour
{
    [Header("Loading")]
    public string nextScene;
    public void Leave()
    {
        SceneManager.LoadScene(nextScene);
    }
}
