using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerBody;
    public GameObject groundRay;

    public float playerMoveSpeed = 30.0f;
    public float playerJumpSpeed = 30.0f;

    float moveInput;
    bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = gameObject.GetComponent<Rigidbody2D>();

        canJump = false;
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        playerBody.velocity = new Vector2(moveInput * playerMoveSpeed, playerBody.velocity.y);

        RaycastHit2D touchingFloor = Physics2D.Raycast (groundRay.transform.position, -Vector2.up);
        Debug.DrawRay(groundRay.transform.position, -Vector2.up * touchingFloor.distance, Color.red);

        if (touchingFloor.collider != null)
        {
            if (touchingFloor.distance <= 0.2)
            {
                canJump = true;
            }
            else
            {
                canJump = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w") || Input.GetKey("space"))
        {
            if (canJump == true)
            {
                playerBody.velocity = Vector2.up * playerJumpSpeed;
            }
            else
            {
                return;
            }
        }
        else if (Input.GetKey("s"))
        {
            Debug.Log(playerBody.position);
        }
    }
}
