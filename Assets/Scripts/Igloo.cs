using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Igloo : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // check object by tag
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Level1_Scene");
        }

    }
}
