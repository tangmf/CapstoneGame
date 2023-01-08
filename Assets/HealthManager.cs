using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    // References
    public Slider healthBar;
    GameMaster gm;
    // Variables
    public float healthPoints;
    public bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        // Set bar to match entity hp
        //healthBar.maxValue = healthPoints;
        // Set to max health
        MaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        //healthBar.transform.rotation = Quaternion.identity;
        /*
        if (Input.GetKeyDown("left"))
        {
            Damage(20);
        }
        if (Input.GetKeyDown("right"))
        {
            Heal(20);
        }
        */
    }

    public void Damage(float dmg)
    {
        healthPoints -= dmg;
        if(healthPoints <= 0)
        {
            healthPoints = 0;
            if (!dead)
            {
                Die();
                dead = true;
            }
            
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
        healthBar.maxValue = healthPoints;
        healthBar.value = healthPoints;
    }

    public void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            GetComponent<PlayerMovement>().animator.SetTrigger("IsDead");
            GetComponent<PlayerMovement>().enabled = !GetComponent<PlayerMovement>().enabled;
            Debug.Log(gameObject.ToString() + " has been killed");
            gm.GameOver("LOSE");
            gm.WaitForRespawn();
        }
        else if(gameObject.CompareTag("Enemy"))
        {
            GetComponent<Boss1Behavior>().animator.SetTrigger("IsDead");
            gm.GameOver("WIN");
        }
        

    }
}
