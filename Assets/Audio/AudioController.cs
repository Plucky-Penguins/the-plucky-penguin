using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public static AudioController aCtrl;
    public GameObject BGM;
    private AudioSource levelMusic;
    public bool switchedScene = false;

    private float defaultVolume = 0.5f;
    public float bgmVolume;
    public float sfxVolume;

    public void Awake()
    {
        // don't destroy this audio controller object
        // so that we can use this object to check levels and change audio as scenes change
        if (aCtrl == null) {
            DontDestroyOnLoad(gameObject);
            setDefaultVolume();
            setLevelMusic();
            aCtrl = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void setLevelMusic() {
        if (levelMusic != null) {
            levelMusic.Stop();
        }
        levelMusic = BGM.GetComponent<AudioSource>();
        levelMusic.loop = true;
        print("Setting lvl music: " + levelMusic.name);
        levelMusic.volume = bgmVolume;
        levelMusic.Play();
    }

    private void setDefaultVolume()
    {
        bgmVolume = defaultVolume;
        sfxVolume = defaultVolume;
    }

    /// <summary>
    /// Level SFX
    /// </summary>
    public void playLevelComplete() {
        GameObject.Find("CompleteLevel").GetComponent<AudioSource>().volume = bgmVolume;
        GameObject.Find("CompleteLevel").GetComponent<AudioSource>().Play();
    }
 
    /// <summary>
    /// Player SFX
    /// </summary>
    public void playJumpSound() {
        GameObject.Find("Jump").GetComponent<AudioSource>().volume = sfxVolume;
        GameObject.Find("Jump").GetComponent<AudioSource>().Play();
    }

    public void playDashSound()
    {
        GameObject.Find("Dash").GetComponent<AudioSource>().volume = sfxVolume;
        GameObject.Find("Dash").GetComponent<AudioSource>().Play();
    }

    public void playAttackSound() {
        GameObject.Find("Shmack").GetComponent<AudioSource>().volume = sfxVolume;
        GameObject.Find("Shmack").GetComponent<AudioSource>().Play();
    }

    public void playCollectFishSound() {
        GameObject.Find("Nom").GetComponent<AudioSource>().volume = sfxVolume;
        GameObject.Find("Nom").GetComponent<AudioSource>().Play();
    }

    public void playHurtSound() {
        GameObject.Find("PenguinHurt").GetComponent<AudioSource>().volume = sfxVolume;
        GameObject.Find("PenguinHurt").GetComponent<AudioSource>().Play();
    }

    /// <summary>
    ///  ABILITY SFX
    /// </summary>
    public void playBurstSound()
    {
        GameObject.Find("Burst").GetComponent<AudioSource>().volume = sfxVolume;
        GameObject.Find("Burst").GetComponent<AudioSource>().Play();
    }

    public void playSpeedSound()
    {
        GameObject.Find("Speed").GetComponent<AudioSource>().volume = sfxVolume;
        GameObject.Find("Speed").GetComponent<AudioSource>().Play();

    }

    public void playSnowballSound() {
        GameObject.Find("Snowball").GetComponent<AudioSource>().volume = sfxVolume;
        GameObject.Find("Snowball").GetComponent<AudioSource>().Play();
    }

    public void playShieldSound() {
        GameObject.Find("Shield").GetComponent<AudioSource>().volume = sfxVolume;
        GameObject.Find("Shield").GetComponent<AudioSource>().Play();
    }

    /// <summary>
    /// BOSS SFX
    /// </summary>
    public void playBossAttack() {
        GameObject.Find("GooseAttack").GetComponent<AudioSource>().volume = sfxVolume;
        GameObject.Find("GooseAttack").GetComponent<AudioSource>().Play();
    }

    public void playBossScreenShake() {
        GameObject.Find("HonkShake").GetComponent<AudioSource>().volume = sfxVolume;
        GameObject.Find("HonkShake").GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    private void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene != "Boss" && switchedScene)
        {
            print(currentScene);
            switch (currentScene) {
                case "Main_Menu":
                case "Store":
                    BGM = GameObject.Find("MenuBGM");
                    setLevelMusic();
                    break;
                case "TutorialLevel_Scene":
                case "Level1_Scene":
                    BGM = GameObject.Find("Tutorial_Lvl1_BGM");
                    setLevelMusic();
                    break;
                case "Level2_Scene":
                case "Level3_Scene":
                    BGM = GameObject.Find("Lvl2_3_BGM");
                    setLevelMusic();
                    break;
                default: 
                    break;
            }
            switchedScene = false;
        }
        else if (currentScene == "Boss" && switchedScene == false) {
            print(currentScene);
            BGM = GameObject.Find("Boss_BGM");
            setLevelMusic();
            switchedScene = true;
        }
        
    }
}
