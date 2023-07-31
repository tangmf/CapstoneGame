using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public bool addMaxHealth = false;
    public bool heal = false;
    public override void AffectCharacter(GameObject character, float val)
    {
        HealthManager health = character.GetComponent<HealthManager>();
        if (health != null){
            if(addMaxHealth){
                health.AddHealth(val);
            }
            if(heal){
                health.Heal(val);
            }
        }
        
            
    }
}
