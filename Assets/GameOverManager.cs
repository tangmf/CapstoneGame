using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;


public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gradeText;
    public TextMeshProUGUI winLoseText;
    public TextMeshProUGUI rewardText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(string winLose, int score, char grade, float winTime, float healthPoints, string charName, int rewardAmt)
    {
        if (winLose == "WIN")
        {
            winLoseText.text = "WIN";
            SaveToJson(score, grade, winTime, healthPoints, charName);
        }
        else
        {
            winLoseText.text = "LOSE";
        }
        ShowScore(score, grade, rewardAmt);
        

    }

    public void ShowScore(int score, char grade,int rewardAmt)
    {
        scoreText.text = score.ToString();
        gradeText.text = grade.ToString();
        rewardText.text = rewardAmt.ToString() + " coins";
    }

    public void SaveToJson(int score,char grade, float winTime, float healthPoints, string charName)
    {
        var currentScene = SceneManager.GetActiveScene();
        var currentSceneName = currentScene.name;


        ScoreDataList datas = new ScoreDataList();
        datas.scoreDatas = new List<ScoreData>();
        ScoreData data = new ScoreData();
        data.sceneName = currentSceneName;
        data.charName = charName;
        data.winTime = winTime;
        data.healthPoints = healthPoints;
        data.score = score;
        data.grade = grade;
        foreach(var i in LoadScoreDatas())
        {
            datas.scoreDatas.Add(i);
        }
        datas.scoreDatas.Add(data);
        Debug.Log(datas.ToString()); 

        string json = JsonUtility.ToJson(datas, true);
        File.WriteAllText(Application.dataPath + "/ScoreDataFile.json", json);
    }

    public List<ScoreData> LoadScoreDatas()
    {

        try
        {
            string json = File.ReadAllText(Application.dataPath + "/ScoreDataFile.json");
            ScoreDataList datas = JsonUtility.FromJson<ScoreDataList>(json);
            return datas.scoreDatas;
        }

        catch
        {
            List<ScoreData> empty = new List<ScoreData>();
            return empty;
        }
            

        
    }
}
