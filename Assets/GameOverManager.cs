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
            if (score >= 950)
            {
                grade = 'S';
            }
            else if (score >= 800)
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
        SaveToJson(score, grade);
        scoreText.text = score.ToString();
        gradeText.text = grade.ToString();

    }

    public void ShowScore(int score)
    {
        char grade;
        if (score >= 950)
        {
            grade = 'S';
        }
        else if (score >= 800)
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
        scoreText.text = score.ToString();
        gradeText.text = grade.ToString();
    }

    public void SaveToJson(int score,char grade)
    {
        var currentScene = SceneManager.GetActiveScene();
        var currentSceneName = currentScene.name;
        ScoreData data = new ScoreData();
        data.sceneName = currentSceneName;
        data.score = score;
        data.grade = grade;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/ScoreDataFile.json", json);
    }
}
