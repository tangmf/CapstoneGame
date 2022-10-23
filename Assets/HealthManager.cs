using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    // References
    public Slider healthBar;
    // Variables
    public float healthPoints;
    // Start is called before the first frame update
    void Start()
    {
        // Set bar to match entity hp
        healthBar.maxValue = healthPoints;
        // Set to max health
        MaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.rotation = Quaternion.identity;
        if (Input.GetKeyDown("left"))
        {
            Damage(20);
        }
        if (Input.GetKeyDown("right"))
        {
            Heal(20);
        }
    }

    public void Damage(float dmg)
    {
        healthPoints -= dmg;
        if(healthPoints <= 0)
        {
            healthPoints = 0;
        }

        healthBar.value = healthPoints;
    }

    public void Heal(float amt)
    {
        healthPoints += amt;
        if (healthPoints >= healthBar.maxValue)
        {
            healthPoints = healthBar.maxValue;
        }

        healthBar.value = healthPoints;
    }

    public void MaxHealth()
    {
        healthBar.value = healthBar.maxValue;
    }
}
