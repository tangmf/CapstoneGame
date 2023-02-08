using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public string target = "Player";
    public bool detectBullets = false;
    public bool detected = true;
    public bool bulletDetected = false;
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
        Debug.Log(collision);
        bool found = false;
        if (collision.gameObject.CompareTag(target))
        {
            found = true;
            detected = true;
        }
        if (detectBullets)
        {
            if (collision.gameObject.GetComponent<BulletBehaviour>())
            {
                found = true;
                bulletDetected = true;
                animator.SetBool("BulletDetected", true);

            }
        }

        if (found)
        {
            animator.SetBool("Detected", true);
        }


    }

    void OnTriggerExit2D(Collider2D collision)
    {
        bool notFound = false;
        if (collision.gameObject.CompareTag(target))
        {
            
            detected = false;
            notFound = true;
        }

        if (detectBullets)
        {
            if (collision.gameObject.GetComponent<BulletBehaviour>())
            {
                bulletDetected = false;
                animator.SetBool("BulletDetected", false);
                notFound = true;

            }
        }

        if (notFound && !bulletDetected && !detected)
        {
            animator.SetBool("Detected", false);
        }
    }


}
