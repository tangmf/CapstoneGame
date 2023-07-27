using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InteractableWords : Interactable
{
    public string textToDisplay;
    public int fontSize = 30;
    public GameObject textPrefab;
    public float timeToShow = 1.0f;

    private GameObject tempTextBox;

    public override void Interact(GameObject go)
    {
        base.Interact(go);
        if (!tempTextBox)
        {
            tempTextBox = Instantiate(textPrefab, this.transform.position, this.transform.rotation);

            // Shift to void Start?
            GameObject textObject = tempTextBox.transform.GetChild(0).gameObject;
            textObject.GetComponent<TMP_Text>().text = textToDisplay;
            textObject.GetComponent<TMP_Text>().fontSize = fontSize;
            
            Destroy(tempTextBox, timeToShow);
            tempTextBox = null;
        }
    }
}
