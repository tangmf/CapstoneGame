using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeNPCs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<NPCBehavior>())
        {
            collision.gameObject.GetComponent<NPCBehavior>().Freeze();
            collision.gameObject.GetComponent<NPCBehavior>().CallUnFreezeInSeconds(5.0f);
        }


    }
}
