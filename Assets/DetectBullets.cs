using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBullets : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BulletBehaviour>())
        { 
            animator.SetTrigger("BulletDetected");

        }


    }
}
