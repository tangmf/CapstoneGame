using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InteractablePopup : Interactable
{
    public GameObject popup;

    public override void Interact(GameObject go)
    {
        base.Interact(go);
        if(go.CompareTag("Player"))
        {
            TogglePopup();
        }


    }

    public void TogglePopup()
    {
        if (popup.activeSelf)
        {
            popup.SetActive(false);
        }
        else
        {
            popup.SetActive(true);
        }
    }

    public override void Update()
    {
        base.Update();
        if (!base.isInArea)
        {
            popup.SetActive(false);
        }
    }

}



