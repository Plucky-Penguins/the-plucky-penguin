using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("BGM Volume Setting")]
    [SerializeField] private Text BGMTextValue = null;
    [SerializeField] private Slider BGMVolumeSlider = null;

    [Header("SFX Volume Setting")]
    [SerializeField] private Text SFXTextValue = null;
    [SerializeField] private Slider SFXVolumeSlider = null;

    [Header("Audio")]
    public AudioController controller;
    public AudioSource BGMMusic;
    // SFX Audio non existent for now
    public AudioSource SFXAudio;

    /**
    *   Specific to BGM Audio Source
    */
    public void SetBGMVolume(float volume)
    {
        BGMTextValue.text = (volume * 100).ToString("0");
        BGMMusic.volume = volume;
    }

    /**
    *   Specific to audio sources of SFX
    */
    public void SetSFXVolume(float volume)
    {
        SFXTextValue.text = (volume * 100).ToString("0");
        SFXAudio.volume = volume;
    }

}
