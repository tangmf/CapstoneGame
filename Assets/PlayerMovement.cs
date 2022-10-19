using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float playerSpeed = 5.0f;
    public float playerJumpHeight = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            rb.velocity = new Vector2(rb.velocity.x, playerJumpHeight);
        }
        else if (Input.GetKey("a"))
        {
            rb.velocity = new Vector2(-playerSpeed, rb.velocity.y);
        }
        else if (Input.GetKey("d"))
        {
            rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
        }
    }
}
