using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InteractableGate : Interactable
{
    public Transform point;

    public override void Interact()
    {
        base.Interact();
        base.player.transform.position = point.position;

    }
}
