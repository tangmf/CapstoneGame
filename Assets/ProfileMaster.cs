using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ProfileMaster : MonoBehaviour
{

    private static ProfileMaster instance;
    public TMP_Text coinText;

    public Image currentCharacterImg;
    public TMP_Text currentCharacterName;
    public GameObject contents;


    Scene firstScene;
    Scene newScene;

    public List<Character> charList;
    public Character currChar;

    // Start is called before the first frame update
    void Start()
    {
        firstScene = SceneManager.GetActiveScene();
        
    }

    // Update is called once per frame
    void Update()
    {
        newScene = SceneManager.GetActiveScene();
        // Is Playable level
        if (newScene.name[0] == 'L' && newScene.name != "LevelSelect" || newScene.name == "MainMenu" || newScene.name == "Options")
        {
            contents.SetActive(false);
        }
        else
        {
            contents.SetActive(true);
            
        }
    }

    void Awake()
    {
        Debug.Log("PM Setup");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        UpdateProfile();

    }

    public void UpdateProfile()
    {
        UpdateCoins();
        UpdateCharacter();
    }

    public void UpdateCoins()
    {
        int coins = 0;
        try
        {
            coins = PlayerPrefs.GetInt("Coins");
        }
        catch
        {
            PlayerPrefs.SetInt("Coins", 0);
        }
        coinText.text = coins.ToString();
        Debug.Log("Coins updated: " + coins);
        
    }

    public void AddCoins(int amt)
    {
        int coins = PlayerPrefs.GetInt("Coins");
        coins += amt;
        PlayerPrefs.SetInt("Coins", coins);
        UpdateCoins();
    }

    public void RemoveCoins(int amt)
    {
        int coins = PlayerPrefs.GetInt("Coins");
        coins -= amt;
        if(coins <= 0)
        {
            coins = 0;
        }
        PlayerPrefs.SetInt("Coins", coins);
        UpdateCoins();
    }

    public void UpdateCharacter()
    {
        string character = "Yomo";
        try
        {
            character = PlayerPrefs.GetString("Char");
            if(character == "")
            {
                character = "Yomo";
            }
        }
        catch
        {
            
        }
        PlayerPrefs.SetString("Char", character);
        currentCharacterName.text = character;
        ChangeCharacter(character);
        Debug.Log("Char updated: " + character);
    }

    public void ChangeCharacter(string name)
    {
        if(name == "Yomo" || name == "Cardena" || name == "Hound")
        {
            PlayerPrefs.SetString("Char", name);
            foreach(Character c in charList)
            {
                if(name == c.name)
                {
                    currentCharacterImg.sprite = c.icon;
                    currChar = c;
                    currentCharacterName.text = currChar.name;
                }
            }
        }
        else
        {
            Debug.Log("Error with changing character");
        }
        

    }
}
