using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class traipsing_anathema_ai : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Physics2D.IgnoreLayerCollision(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
