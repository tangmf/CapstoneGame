using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreContainer : MonoBehaviour
{
    public TextMeshProUGUI gradeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI winTimeText;
    public TextMeshProUGUI hpLeftText;
    public Image icon;
    public Sprite yomo;
    public Sprite hound;
    public Sprite cardena;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set(ScoreData data, int rank)
    {
        gradeText.text = data.grade.ToString();
        scoreText.text = data.score.ToString();
        winTimeText.text = data.winTime.ToString("F2");
        hpLeftText.text = data.healthPoints.ToString();
        rankText.text = rank.ToString();

        if(data.charName == "Yomo")
        {
            icon.sprite = yomo;
        }
        else if (data.charName == "Cardena")
        {
            icon.sprite = cardena;
        }
        else if (data.charName == "Hound")
        {
            icon.sprite = hound;
        }


    }
}
