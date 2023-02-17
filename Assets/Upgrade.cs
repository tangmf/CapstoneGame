using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpgradeHealth(int amt)
    {
        player.GetComponent<HealthManager>().healthBar.maxValue += amt;
        player.GetComponent<HealthManager>().Heal(amt);
        Resume();
        
    }

    public void HealMax()
    {
        player.GetComponent<HealthManager>().Heal(player.GetComponent<HealthManager>().healthBar.maxValue);
        Resume();

    }

    public void UpradeAtk(int amt)
    {
        player.GetComponent<PlayerBehaviour>().damage += amt;
        Resume();
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        Destroy(gameObject);
    }
}
