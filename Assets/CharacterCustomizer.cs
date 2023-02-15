using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCustomizer : MonoBehaviour
{
    public Character currChar;

    public Image model;
    public TMP_Text nameText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCharacter(Character c)
    {
        currChar = c;
        UpdateAll();
    }

    public void UpdateAll()
    {
        model.sprite = currChar.imgModel;
        nameText.text = currChar.name;
    }
}
