using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Behavior : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;
    Collider2D collider;

    Transform player;

    public bool isFlipped = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        Physics2D.IgnoreCollision(collider, player.GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}
