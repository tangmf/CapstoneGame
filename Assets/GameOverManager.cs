using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        if (winLose == "WIN")
        {
            winLoseText.text = "WIN";
            ShowScore(score);
        }
        else
        {
            winLoseText.text = "LOSE";
            scoreText.text = score.ToString();
            gradeText.text = "F";
        }
        
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
}
