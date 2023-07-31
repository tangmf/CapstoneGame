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

        public enum Slots{
            Weapon,
            Chest
        }
        public Slots slot = Slots.Weapon;

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
            if (weaponSystem != null)
            {
                if(slot == Slots.Weapon){
                    weaponSystem.SetWeapon(this, itemState == null ? 
                    DefaultParametersList : itemState);
                    return true;
                }
                else if(slot == Slots.Chest){
                    weaponSystem.SetChest(this, itemState == null ? 
                    DefaultParametersList : itemState);
                    return true;
                }
                
            }

            LogManager.instance.Log(ActionName + " " + base.name);
            return false;
        }
    }
}