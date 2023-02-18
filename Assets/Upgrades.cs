using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrades : MonoBehaviour
{
    ProfileMaster profileMaster;
    public TextMeshProUGUI boxLevel;
    public TextMeshProUGUI boxCost;

    public int amount;
    public int cost;
    public int level;

    public bool isDamage;
    public bool isHealth;
    public bool isSpeed;

    // Start is called before the first frame update
    void Start()
    {
        profileMaster = GameObject.FindGameObjectWithTag("PM").GetComponent<ProfileMaster>();
        SetDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonClicked()
    {
        if (isDamage)
        {
            level = PlayerPrefs.GetInt("DamageLevel");
            cost = PlayerPrefs.GetInt("DamageCost");
            Upgrade("Damage", "DamageLevel", "DamageCost", level, cost, 2);
        }
        else if (isHealth)
        {
            level = PlayerPrefs.GetInt("HealthLevel");
            cost = PlayerPrefs.GetInt("HealthCost");
            Upgrade("Health", "HealthLevel", "HealthCost", level, cost, 30);
        }
        else if (isSpeed)
        {
            level = PlayerPrefs.GetInt("SpeedLevel");
            cost = PlayerPrefs.GetInt("SpeedCost");
            Upgrade("Speed", "SpeedLevel", "SpeedCost", level, cost, 3);
        }
    }

    public void Upgrade(string typeName, string typeLevel, string typeCost, int level, int cost, int increase)
    {
        if (level < 3)
        {
            bool purchase = Purchase(cost + 200);

            if (purchase)
            {
                PlayerPrefs.SetInt(typeName, increase * level);
                PlayerPrefs.SetInt(typeLevel, level + 1);
                PlayerPrefs.SetInt(typeCost, 50 * (level + 1));

                SetDisplay();
            }
        }
    }

    bool Purchase(int cost)
    {
        int coins = PlayerPrefs.GetInt("Coins");

        if (coins >= cost)
        {
            profileMaster.RemoveCoins(cost);
            return true;
        }
        else
        {
            return false;
        }
    }

    void SetDisplay()
    {
        if (isDamage)
        {
            boxLevel.text = "Damage Level " + PlayerPrefs.GetInt("DamageLevel").ToString();
            boxCost.text = (PlayerPrefs.GetInt("DamageCost") + 200).ToString();
            if (PlayerPrefs.GetInt("DamageLevel") == 3) { boxCost.text = "MAX"; gameObject.SetActive(false); }
        }
        else if (isHealth)
        {
            boxLevel.text = "Health Level " + PlayerPrefs.GetInt("HealthLevel").ToString();
            boxCost.text = (PlayerPrefs.GetInt("DamageCost") + 200).ToString();
            if (PlayerPrefs.GetInt("HealthLevel") == 3) { boxCost.text = "MAX"; gameObject.SetActive(false); }
        }
        else if (isSpeed)
        {
            boxLevel.text = "Speed Level " + PlayerPrefs.GetInt("SpeedLevel").ToString();
            boxCost.text = (PlayerPrefs.GetInt("DamageCost") + 200).ToString();
            if (PlayerPrefs.GetInt("SpeedLevel") == 3) { boxCost.text = "MAX"; gameObject.SetActive(false); }
        }
    }
}
