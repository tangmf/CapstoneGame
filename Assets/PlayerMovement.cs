using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerBody;
    public GameObject groundRay;
    public LayerMask layerMask;

    public float playerMoveSpeed;
    public float playerJumpSpeed;
    public float jumpFloorDistance;

    float moveInput;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = gameObject.GetComponent<Rigidbody2D>();

        playerMoveSpeed = 8.0f;
        playerJumpSpeed = 15.0f;
        jumpFloorDistance = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        playerBody.velocity = new Vector2(moveInput * playerMoveSpeed, playerBody.velocity.y);
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (Input.GetKeyDown("w") || Input.GetKeyDown("space"))
        {
            RaycastHit2D touchingFloor = Physics2D.Raycast(groundRay.transform.position, -Vector2.up, jumpFloorDistance, layerMask);

            if (touchingFloor.collider != null)
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
