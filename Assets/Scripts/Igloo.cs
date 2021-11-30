using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Igloo : MonoBehaviour
{
    public TMPro.TextMeshProUGUI winText;
    public string nextScene;

    public static string currentLevel;

    private void Start()
    {
        winText.enabled = false;
        currentLevel = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check object by tag
        if (collision.gameObject.tag == "Player")
        {
            // TODO: play level completion sfx
            // TODO: display score
            StartCoroutine(ShowMessage("You Win!"));
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject.Find("Player").GetComponent<Rigidbody2D>().position = transform.position;
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
