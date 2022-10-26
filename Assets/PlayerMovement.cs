using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    Rigidbody2D playerBody;
    public GameObject groundRay;
    public LayerMask layerMask;

    public float playerMoveSpeed = 8.0f;
    public float playerJumpSpeed = 17.5f;
    public float jumpFloorDistance = 0.2f;

    float moveInput;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        if(moveInput != 0)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
        
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
                animator.SetBool("Jumping", true);
            }
            else
            {
                return;
            }
        }
        else
        {
            animator.SetBool("Jumping", false);
        }
        if (Input.GetKey("s"))
        {
            Debug.Log(playerBody.position);
            animator.SetBool("Crouching", true);
        }
        else
        {
            animator.SetBool("Crouching", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Shift", true);
        }
        else
        {
            animator.SetBool("Shift", false);
        }

    }
}
