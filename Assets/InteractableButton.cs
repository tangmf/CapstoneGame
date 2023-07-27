using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractableButton : Interactable
{

    public override void Interact()
    {
        base.Interact();
        this.GetComponent<Button>().onClick.Invoke();

    }
}
