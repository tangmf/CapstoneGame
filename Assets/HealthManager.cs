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
    public AudioClip hitSfx;
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
        /*
        if (gameObject.CompareTag("Player"))
        {
            PlayerBehaviour player = gameObject.GetComponent<PlayerBehaviour>();
            player.animator.SetTrigger("Damaged");
        }
        */
        StartCoroutine(Damaged());
        if (hitSfx != null)
        {
            AudioSource.PlayClipAtPoint(hitSfx, this.gameObject.transform.position);
        }
        if (healthPoints <= 0)
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

    IEnumerator Damaged()
    {
        Debug.Log("HIT");
        Color prevColor = gameObject.GetComponent<SpriteRenderer>().color;
        Color tmp = prevColor;
        if (gameObject.CompareTag("Player"))
        {
            
            tmp = Color.red;
            gameObject.GetComponent<SpriteRenderer>().material.color = tmp;
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<SpriteRenderer>().material.color = prevColor;

        }
        else
        {
            tmp.a = 0.7f;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<SpriteRenderer>().color = prevColor;
        }

        

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
            if (GetComponent<BossBehavior>())
            {
                GetComponent<BossBehavior>().animator.SetTrigger("Is_Dead");
            }
            
            gm.GameOver("WIN");
        }
        

    }
}
