using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CheckIfAllCompleted : MonoBehaviour
{
    public string finalScene = "L4";
    public GameObject finalDialogue;
    // Start is called before the first frame update
    void Start()
    {
        finalDialogue.SetActive(false);
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


        foreach (ScoreData data in datas.scoreDatas)
        {
            if (data.sceneName == finalScene)
            {
                finalDialogue.SetActive(true);
                break;

            }

        }


    }
}
