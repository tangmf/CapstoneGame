using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Inventory.Model;

public class EquipmentSlotUI : MonoBehaviour
{
    private EquippableItemSO equipment;
    public Image icon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set(EquippableItemSO e){
        equipment = e;
        icon.sprite = e.ItemImage;
    }
}
