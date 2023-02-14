using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    List<CharContainer> charList = new List<CharContainer>();

    public CharacterCustomizer cCustomizer;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform x in transform)
        {
            charList.Add(x.gameObject.GetComponent<CharContainer>());
        }
        PlayerPrefs.SetString("Yomo", "Unlocked");

        UpdateAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAll()
    {
        foreach(CharContainer cc in charList)
        {
            if (IsCharUnlocked(cc.character.name))
            {
                cc.Unlock();
            }
            else
            {
                cc.Lock();
            }
        }
    }

    public bool IsCharUnlocked(string name)
    {
        if (PlayerPrefs.GetString(name) == "Unlocked")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
