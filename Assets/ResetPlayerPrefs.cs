using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ResetPlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ResetScores()
    {
        try
        {
            string json = JsonUtility.ToJson("", true);
            File.WriteAllText(Application.dataPath + "/ScoreDataFile.json", json);
        }
        catch
        {

        }


    }
}
