using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDetection : MonoBehaviour
{
    public GameObject dialogue;
    // Start is called before the first frame update
    void Start()
    {
        dialogue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogue.SetActive(true);
        }


    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
                dialogue.SetActive(false);
        }
    }
}
