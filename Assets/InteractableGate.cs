using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InteractableGate : Interactable
{
    public Transform point;

    public override void Interact(GameObject go)
    {
        base.Interact(go);
        go.transform.position = point.position;

    }
}
