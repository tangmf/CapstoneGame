using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI grade;
    public Image img;
    public SceneItem sceneItem;
    Menu m;
    public GameObject lockObject;
    public GameObject scoreBoard;
    public TextMeshProUGUI levelText;
    
    ScoreDataList datas;
    // Start is called before the first frame update
    void Start()
    {
        m = GameObject.FindWithTag("Menu").GetComponent<Menu>();
        scoreBoard = GameObject.FindWithTag("Scoreboard");
        scoreBoard.transform.GetChild(0).gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadFromJson()
    {
        var scene = sceneItem.scene;
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
        if(scene == "L0" )
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
        scoreBoard.transform.GetChild(0).gameObject.SetActive(true);
        scoreBoard.GetComponent<ScoreBoardManager>().ShowScores(levelText.text, datas, sceneItem.scene);
    }

    public void SetUp(SceneItem si)
    {
        sceneItem = si;
        levelText.text = si.name;
        if(si.type == "Survival")
        {
            img.sprite = Resources.Load<Sprite>("Images/survivalIcon");
        }
        LoadFromJson();
    }

    public void StartLevel()
    {
        m.LoadScreenByName(sceneItem.scene);
    }
}
