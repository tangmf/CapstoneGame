using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreContainer : MonoBehaviour
{
    public TextMeshProUGUI gradeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI rankText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set(char grade, int score, int rank)
    {
        gradeText.text = grade.ToString();
        scoreText.text = score.ToString();
        rankText.text = rank.ToString();
    }
}
