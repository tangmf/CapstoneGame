using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI grade;
    public string scene;
    public GameObject lockObject;
    public GameObject scoreBoard;
    public TextMeshProUGUI levelText;
    
    ScoreDataList datas;
    // Start is called before the first frame update
    void Start()
    {
        scoreBoard.SetActive(false);
        LoadFromJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/ScoreDataFile.json");
        datas = JsonUtility.FromJson<ScoreDataList>(json);
        ScoreData highScore = null;

        string[] splitArray = scene.Split(char.Parse("L"));
        string newNumber = (splitArray[1]);
        Debug.Log(newNumber);
        string prevSceneName = "L0";
        try
        {
            prevSceneName = "L" + (Convert.ToInt32(newNumber) - 1).ToString();
        }
        catch
        {
            
        }
        if(scene == "L0")
        {
            RemoveLock();
        }

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
            else if(data.sceneName == prevSceneName)
            {
                RemoveLock();
            }
        }
        if(highScore != null)
        {
            Debug.Log(highScore.score);
            grade.text = highScore.grade.ToString();
            RemoveLock();
        }
        
    }

    public void RemoveLock()
    {
        lockObject.SetActive(false);
    }

    public void ShowScoreBoard()
    {
        scoreBoard.SetActive(true);
        scoreBoard.GetComponent<ScoreBoardManager>().ShowScores(levelText.text, datas, scene);
    }
}
