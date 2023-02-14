using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharContainer : MonoBehaviour
{
    ProfileMaster pm;
    public Character character;
    public TMP_Text charNameText;
    public GameObject locker;
    public TMP_Text price;

    public CharacterManager cm;
    // Start is called before the first frame update
    void Start()
    {
        charNameText.text = character.name;
        pm = GameObject.FindWithTag("PM").GetComponent<ProfileMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectCharacter()
    {
        if(PlayerPrefs.GetString(character.name) == "Unlocked")
        {
            pm.ChangeCharacter(character.name);
        }
        
    }

    public void Lock()
    {
        locker.SetActive(true);
        price.text = character.cost.ToString();
    }

    public void Unlock()
    {
        locker.SetActive(false);
    }

    public void UnlockCharacter()
    {
        int coins = PlayerPrefs.GetInt("Coins");
        if (coins >= character.cost)
        {
            pm.RemoveCoins(character.cost);
            PlayerPrefs.SetString(character.name, "Unlocked");
            cm.UpdateAll();
        }
        
    }
}
