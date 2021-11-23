using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController aCtrl;
    public GameObject BGM;
    public AudioSource sfx;
    private AudioSource levelMusic;

    public void Awake()
    {
        if (aCtrl == null) {
            levelMusic = BGM.GetComponent<AudioSource>();
            levelMusic.volume = 0.5f;
            levelMusic.loop = true;
            aCtrl = this;
        }
    }
}
