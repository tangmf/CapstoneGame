using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InteractableGate : Interactable
{
    public Transform point;
    public GameObject overlay;
    public float overlayTime = 0.5f;
    
    public override void Interact(GameObject go)
    {
        base.Interact(go);

        // only trigger animation if player
        if(go.CompareTag("Player")){
            StartOverlay();
        }
        
        go.transform.position = point.position;

    }

    public void StartOverlay(){
        GameObject newObj = Instantiate(overlay, new Vector3(0,0,0), Quaternion.identity);
        newObj.transform.parent = this.transform;
    }
}
