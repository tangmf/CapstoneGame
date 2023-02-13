using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharContainer : MonoBehaviour
{
    ProfileMaster pm;
    public string charName;
    public TMP_Text charNameText;
    // Start is called before the first frame update
    void Start()
    {
        charNameText.text = charName;
        pm = GameObject.FindWithTag("PM").GetComponent<ProfileMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectCharacter()
    {
        pm.ChangeCharacter(charName);
    }
}
