using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;

public class ScriptReader : MonoBehaviour
{
    [SerializeField]
    private TextAsset inkFile;
    private Story story;

    public TMP_Text dialogueBox;
    public TMP_Text nameTag;

    public Image charPortrait;

    public GameObject nextBtn;

    // Start is called before the first frame update
    void Start()
    {
        string isRead = PlayerPrefs.GetString(inkFile.name);
        if(isRead == "Done")
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            LoadStory();

            DisplayNextLine();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextLine();
        }
    }

    void LoadStory()
    {
        story = new Story(inkFile.text);
        story.BindExternalFunction("Name", (string charName)=> ChangeName(charName));
        story.BindExternalFunction("Portrait", (string charName) => CharacterIcon(charName));

    }

    public void DisplayNextLine()
    {
        if (story.canContinue)
        {
            string text = story.Continue();
            text = text?.Trim();
            dialogueBox.text = text;
            if (!story.canContinue)
            {
                PlayerPrefs.SetString(inkFile.name, "Done");
                Destroy(nextBtn);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeName(string name)
    {
        string speakerName = name;

        nameTag.text = speakerName;

    }

    public void CharacterIcon(string name)
    {
        var charIcon = Resources.Load<Sprite>("CharPortraits/"+name);
        charPortrait.sprite = charIcon;
    }

    public void Close()
    {
        PlayerPrefs.SetString(inkFile.name, "Done");
        gameObject.SetActive(false);
    }
}
