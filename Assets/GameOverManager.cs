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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(string winLose, int score)
    {
        char grade;
        if (winLose == "WIN")
        {
            winLoseText.text = "WIN";
            if (score >= 1500)
            {
                grade = 'S';
            }
            else if (score >= 1000)
            {
                grade = 'A';
            }
            else if (score >= 600)
            {
                grade = 'B';
            }
            else if (score >= 400)
            {
                grade = 'C';
            }
            else
            {
                grade = 'D';
            }
            SaveToJson(score, grade);
        }
        else
        {
            winLoseText.text = "LOSE";
            if (score < 0)
            {
                score = 0;
            }
            grade = 'F';
        }
        scoreText.text = score.ToString();
        gradeText.text = grade.ToString();
        

    }

    public void ShowScore(int score)
    {
        char grade;
        if (score >= 1900)
        {
            grade = 'S';
        }
        else if (score >= 1500)
        {
            grade = 'A';
        }
        else if (score >= 1000)
        {
            grade = 'B';
        }
        else if (score >= 500)
        {
            grade = 'C';
        }
        else
        {
            grade = 'D';
        }
        scoreText.text = score.ToString();
        gradeText.text = grade.ToString();
    }

    public void SaveToJson(int score,char grade)
    {
        var currentScene = SceneManager.GetActiveScene();
        var currentSceneName = currentScene.name;


        ScoreDataList datas = new ScoreDataList();
        datas.scoreDatas = new List<ScoreData>();
        ScoreData data = new ScoreData();
        data.sceneName = currentSceneName;
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
