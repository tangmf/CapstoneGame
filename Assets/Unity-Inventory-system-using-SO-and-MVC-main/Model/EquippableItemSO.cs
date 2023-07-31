using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";

        [field: SerializeField]
        public AudioClip actionSFX { get; private set; }

        public EnumList.Slot slot = EnumList.Slot.Weapon;

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
            if (weaponSystem != null)
            {
                weaponSystem.Set(slot, this, itemState == null ? 
                    DefaultParametersList : itemState);
                    return true;
                
            }

            LogManager.instance.Log(ActionName + " " + base.name);
            return false;
        }
    }
}