using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New sceneItem", menuName = "SceneItem")]
public class SceneItem : ScriptableObject

{
    public string type;
    public new string name;
    public string scene;

}
