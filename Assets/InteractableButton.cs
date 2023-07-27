using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractableButton : Interactable
{

    public override void Interact(GameObject go)
    {
        base.Interact(go);
        this.GetComponent<Button>().onClick.Invoke();

    }
}
