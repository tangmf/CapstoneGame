using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ResetPlayerPrefs : MonoBehaviour
{
    public Menu menu;

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

        // Reset Upgrades to normal
        ResetUpgrades();

        // Return to Main Menu
        menu.LoadSceneByName("MainMenu");
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

    void ResetUpgrades()
    {
        PlayerPrefs.SetInt("Damage", 10);
        PlayerPrefs.SetInt("DamageLevel", 0);
        PlayerPrefs.SetInt("DamageCost", 200);

        PlayerPrefs.SetInt("Health", 200);
        PlayerPrefs.SetInt("HealthLevel", 0);
        PlayerPrefs.SetInt("HealthCost", 200);

        PlayerPrefs.SetInt("Speed", 20);
        PlayerPrefs.SetInt("SpeedLevel", 0);
        PlayerPrefs.SetInt("SpeedCost", 200);
    }
}
