using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InteractableVendor : Interactable
{
    public Transform point;
    public Item item;

    public override void Interact(GameObject go)
    {
        base.Interact(go);

        Instantiate(item, point.position, Quaternion.identity);

    }

}
