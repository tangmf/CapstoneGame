using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject

{
    public new string name;
    public Sprite icon;
    public Sprite imgModel;
    public int cost = 0;
    public GameObject playerModel;

}
