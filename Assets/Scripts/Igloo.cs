using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Igloo : MonoBehaviour
{
    public TMPro.TextMeshProUGUI winText;
    public string nextScene;

    private void Start()
    {
        winText.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check object by tag
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(ShowMessage("You Win!"));
            
        }

    }
    
    IEnumerator ShowMessage(string message)
    {
        winText.enabled = true;
        yield return new WaitForSeconds(2);
        winText.enabled = false;
        SceneManager.LoadScene(nextScene);
    }
}
