using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class accursed_anathema_ai : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;
    public Transform player;

    public bool isFlipped = false;

    // Start is called before the first frame update
    void Start()
    {
        body = this.transform.GetChild(0).GetComponent<Rigidbody2D>();
        animator = this.transform.GetChild(0).GetComponent<Animator>();

        Physics2D.IgnoreLayerCollision(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LookAtPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        Debug.Log("testtest");

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
            Debug.Log("testfalse");
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
            Debug.Log("testtrue");
        }
    }
}
