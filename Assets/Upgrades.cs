using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrades : MonoBehaviour
{
    public ProfileMaster profileMaster;
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
            amount = PlayerPrefs.GetInt("Damage");
            level = PlayerPrefs.GetInt("DamageLevel");
            cost = PlayerPrefs.GetInt("DamageCost");
            Upgrade("Damage", "DamageLevel", "DamageCost", amount, level, cost, 2);
        }
        else if (isHealth)
        {
            amount = PlayerPrefs.GetInt("Health");
            level = PlayerPrefs.GetInt("HealthLevel");
            cost = PlayerPrefs.GetInt("HealthCost");
            Upgrade("Health", "HealthLevel", "HealthCost", amount, level, cost, 30);
        }
        else if (isSpeed)
        {
            amount = PlayerPrefs.GetInt("Speed");
            level = PlayerPrefs.GetInt("SpeedLevel");
            cost = PlayerPrefs.GetInt("SpeedCost");
            Upgrade("Speed", "SpeedLevel", "SpeedCost", amount, level, cost, 3);
        }
    }

    public void Upgrade(string typeName, string typeLevel, string typeCost, int amount, int level, int cost, int increase)
    {
        if (level < 3)
        {
            bool purchase = Purchase(cost);

            if (purchase)
            {
                PlayerPrefs.SetInt(typeName, amount + increase);
                PlayerPrefs.SetInt(typeLevel, level + 1);
                PlayerPrefs.SetInt(typeCost, cost + (50 * (level + 1)));

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
            boxCost.text = PlayerPrefs.GetInt("DamageCost").ToString();
            if (PlayerPrefs.GetInt("DamageLevel") == 3) { boxCost.text = "MAX"; gameObject.SetActive(false); }
        }
        else if (isHealth)
        {
            boxLevel.text = "Health Level " + PlayerPrefs.GetInt("HealthLevel").ToString();
            boxCost.text = PlayerPrefs.GetInt("HealthCost").ToString();
            if (PlayerPrefs.GetInt("HealthLevel") == 3) { boxCost.text = "MAX"; gameObject.SetActive(false); }
        }
        else if (isSpeed)
        {
            boxLevel.text = "Speed Level " + PlayerPrefs.GetInt("SpeedLevel").ToString();
            boxCost.text = PlayerPrefs.GetInt("SpeedCost").ToString();
            if (PlayerPrefs.GetInt("SpeedLevel") == 3) { boxCost.text = "MAX"; gameObject.SetActive(false); }
        }
    }
}
