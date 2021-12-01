using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController aCtrl;
    public GameObject BGM;
    private AudioSource levelMusic;

    public static AudioController audioController;

    public void Awake()
    {
        // don't destroy this audio controller object
        // so that we can use this object to check levels and change audio as scenes change
        if (audioController == null)
        {
            DontDestroyOnLoad(gameObject);
            audioController = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (aCtrl == null) {
            levelMusic = BGM.GetComponent<AudioSource>();
            levelMusic.volume = 0.5f;
            levelMusic.loop = true;
            aCtrl = this;
        }
    }


    // Update is called once per frame
    private void Update()
    {
        
    }
}
