using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject item;
    public float dropchance = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;
        if(Random.Range(0.0f, 1.0f) < dropchance)
        {
            Instantiate(item, transform.position, transform.rotation);
        }
        
    }
}
