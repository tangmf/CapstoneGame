using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{


    [SerializeField]
    private InventorySO inventoryData;

    EquipmentSlot weaponSlot; 
    EquipmentSlot headSlot; 
    EquipmentSlot chestSlot;
    EquipmentSlot legSlot;

    public PlayerEquipment pe;
    
    public class EquipmentSlot : MonoBehaviour {
        //Variable declaration
        //Note: I'm explicitly declaring them as public, but they are public by default. You can use private if you choose.
        [SerializeField]
        public EquippableItemSO itemSO;

        [SerializeField]
        public List<ItemParameter> parametersToModify, itemCurrentState;

        public EnumList.Slot slot;
    
        //Constructor (not necessary, but helpful)
        public EquipmentSlot(EnumList.Slot s, EquippableItemSO item, List<ItemParameter> paras, List<ItemParameter> currState) {
            this.itemSO = item;
            this.parametersToModify = paras;
            this.itemCurrentState = currState;
            this.slot = s;
     
     
        }
    }


    void Start(){
        weaponSlot = new EquipmentSlot(EnumList.Slot.Weapon, null, null, null);
        headSlot = new EquipmentSlot(EnumList.Slot.Head, null, null, null);
        chestSlot = new EquipmentSlot(EnumList.Slot.Chest, null, null, null);
        legSlot = new EquipmentSlot(EnumList.Slot.Leg, null, null, null);
    }

    public void Set(EnumList.Slot s, EquippableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        EquipmentSlot es = new EquipmentSlot(EnumList.Slot.Weapon, null, null, null);
        if(s == EnumList.Slot.Head){
            es = headSlot;
            pe.head.Set(weaponItemSO);
        }
        else if(s == EnumList.Slot.Chest){
            es = chestSlot;
            pe.chest.Set(weaponItemSO);
        }
        else if(s == EnumList.Slot.Leg){
            es = legSlot;
            pe.leg.Set(weaponItemSO);
        }
        else if(s == EnumList.Slot.Weapon){
            es = weaponSlot;
            pe.weapon.Set(weaponItemSO);
        }

        if (es.itemSO != null)
        {
            inventoryData.AddItem(es.itemSO, 1, es.itemCurrentState);
        }

        es.itemSO = weaponItemSO;
        es.itemCurrentState = new List<ItemParameter>(itemState);
        ModifyParameters(es);
    }

    private void ModifyParameters(EquipmentSlot es)
    {
        foreach (var parameter in es.parametersToModify)
        {
            if (es.itemCurrentState.Contains(parameter))
            {
                int index = es.itemCurrentState.IndexOf(parameter);
                float newValue = es.itemCurrentState[index].value + parameter.value;
                es.itemCurrentState[index] = new ItemParameter
                {
                    itemParameter = parameter.itemParameter,
                    value = newValue
                };
            }
        }
    }
}
