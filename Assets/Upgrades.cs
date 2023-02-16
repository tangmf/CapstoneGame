using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    ProfileMaster profileMaster;

    public int damageCost;
    public int healthCost;
    public int speedCost;

    public int damageLevel;
    public int healthLevel;
    public int speedLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpgradeDamage()
    {
        bool purchase = Purchase(damageCost);

        if (purchase)
        {
            if (damageLevel <= 3)
            {
                int damage = PlayerPrefs.GetInt("Damage");

                PlayerPrefs.SetInt("Damage", damage * (1/5));
                damageCost = damageCost * 1/3;
            }
        }
    }

    void UpgradeHealth()
    {
        bool purchase = Purchase(healthCost);

        if (purchase)
        {
            if (healthLevel <= 3)
            {
                int health = PlayerPrefs.GetInt("Health");

                PlayerPrefs.SetInt("Health", health * 1/5);
                healthCost = healthCost * 1/3;
            }
        }
    }

    void UpgradeSpeed()
    {
        bool purchase = Purchase(speedCost);

        if (purchase)
        {
            if (speedLevel <= 3)
            {
                int speed = PlayerPrefs.GetInt("Speed");

                PlayerPrefs.SetInt("Speed", speed * 1 / 5);
                speedCost = speedCost * 1 / 3;
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
}
