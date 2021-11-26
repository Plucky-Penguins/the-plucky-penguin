using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class HighScoreScript : MonoBehaviour
{
    public static float score;
    public static int highscore = 99999999;
    const string filename = "/highscore.dat"; // file is at "[user]/AppData/LocalLow/[company name]/[game name]
    // Start is called before the first frame update
    void Awake()
    {
        loadScore(); // load saved high score
    }

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime; // increases score by 1 every second

        // this was just for debugging and might be useful if there are bugs
/*        if (Input.GetKeyDown(KeyCode.O)) 
        {
            Debug.Log("current score: " + Mathf.RoundToInt(score));
            Debug.Log("highscore: " + highscore);
        }*/
    }

    [Serializable]
    class GameData
    {
        public int score = 99999999; // set the initial score super high so it's beatable
    };

    public void loadScore() // load score function from lab
    {
        if (File.Exists(Application.persistentDataPath + filename))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + filename, FileMode.Open, FileAccess.Read);
            GameData data = (GameData)bf.Deserialize(fs);
            fs.Close();
            highscore = data.score;

        }
    }

    public void saveScore() // save score function from lab
    {
        // save the current highscore
        if (score < highscore)
        {
            highscore = Mathf.RoundToInt(score);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + filename, FileMode.OpenOrCreate);
            GameData data = new GameData();
            data.score = highscore;
            bf.Serialize(fs, data);
            fs.Close();
        }
    }
}
