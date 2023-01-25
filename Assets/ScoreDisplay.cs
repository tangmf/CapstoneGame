using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI grade;
    public string scene;
    // Start is called before the first frame update
    void Start()
    {
        LoadFromJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/ScoreDataFile.json");
        ScoreDataList datas = JsonUtility.FromJson<ScoreDataList>(json);
        ScoreData highScore = null;
        foreach(ScoreData data in datas.scoreDatas)
        {
            if(data.sceneName == scene)
            {
                if(highScore == null)
                {
                    highScore = data;
                }
                else
                {
                    if(highScore.score < data.score)
                    {
                        highScore = data;
                    }
                }
                
            }
        }
        if(highScore != null)
        {
            Debug.Log(highScore.score);
            grade.text = highScore.grade.ToString();
        }
        
    }
}
