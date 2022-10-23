using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public string target = "Player";
    public bool detected = true;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        detected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag(target))
        {
            animator.SetBool("Detected", true);
            detected = true;
        }


    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(target))
        {
            animator.SetBool("Detected", false);
            detected = false;
        }
    }


}
