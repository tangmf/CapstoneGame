using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerBody;
    public CapsuleCollider2D playerCollider;
    public Vector2 playerPosition;
    public float playerSpeed = 5.0f;
    public float playerJumpHeight = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = gameObject.GetComponent<Rigidbody2D>();
        playerCollider = gameObject.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w") || Input.GetKeyDown("space"))
        {
            int results = playerCollider.Raycast(playerBody.position, new RaycastHit2D test[]);
            playerBody.velocity = new Vector2(playerBody.velocity.x, playerJumpHeight);
        }
        else if (Input.GetKey("a"))
        {
            playerBody.velocity = new Vector2(-playerSpeed, playerBody.velocity.y);
        }
        else if (Input.GetKey("s"))
        {
            Debug.Log(playerBody.position);
        }
        else if (Input.GetKey("d"))
        {
            playerBody.velocity = new Vector2(playerSpeed, playerBody.velocity.y);
        }
    }
}
