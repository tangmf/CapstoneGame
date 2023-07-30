using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InteractableItem : Interactable
{
    [SerializeField]
    Item item;

    public override void Interact(GameObject go)
    {
        base.Interact(go);
        if (item && go.CompareTag("Player"))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        Debug.Log("Picking up item");
        //Inventory.instance.Add(item);
        Destroy(gameObject);
    }
}
