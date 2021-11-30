using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseHandler : MonoBehaviour
{
    // Start is called before the first frame update
    static bool gamePaused;
    public GameObject pauseMenu;
    public Text pauseText;
    public Button exitButton;
    void Start()
    {
        pauseMenu.GetComponent<Renderer>().enabled = false;
        pauseText.enabled = false;
        exitButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = !gamePaused;
            if (gamePaused)
            {
                Time.timeScale = 0;
                pauseMenu.GetComponent<Renderer>().enabled = true;
                pauseText.enabled = true;
                exitButton.gameObject.SetActive(true);
            } else
            {
                Time.timeScale = 1;
                pauseMenu.GetComponent<Renderer>().enabled = false;
                pauseText.enabled = false;
                exitButton.gameObject.SetActive(false);
            }
            
        }
    }

    public void closeGame()
    {
        Application.Quit();
    }
}
