using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    Rigidbody2D playerBody;
    public GameObject groundRay;
    public LayerMask layerMask;

    public float playerMoveSpeed = 8.0f;
    public float playerJumpSpeed = 17.5f;
    public float jumpFloorDistance = 0.2f;

    float moveInput;
    public float dashCD = 0.5f;
    public float nextDash = 0.0f;
    public bool grounded = false;

    bool jumpKeyHeld;
    bool isJumping = false;
    public Vector2 counterJumpForce = new Vector2(0,-30);

    public ParticleSystem dust;

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
        RaycastHit2D touchingFloor = Physics2D.Raycast(groundRay.transform.position, -Vector2.up, jumpFloorDistance, layerMask);
        if (touchingFloor.collider != null)
        {
            animator.SetBool("Grounded", true);
            grounded = true;
        }
        else
        {
            animator.SetBool("Grounded", false);
            grounded = false;
        }

        if (moveInput != 0)
        {
            CreateDust();
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
            jumpKeyHeld = true;
            if (grounded)
            {
                isJumping = true;
                playerBody.velocity = Vector2.up * playerJumpSpeed;
                animator.SetBool("Jumping", true);
            }

        }
        else if (Input.GetKeyUp("w") || Input.GetKeyUp("space"))
        {
            jumpKeyHeld = false;
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
        if(Time.time >= nextDash)
        {
            animator.SetBool("CanDash", true);
        }
        else
        {
            animator.SetBool("CanDash", false);
        }
    }

    void FixedUpdate()
    {
        if (isJumping)
        {
            if (!jumpKeyHeld && Vector2.Dot(playerBody.velocity, Vector2.up) > 0)
            {
                playerBody.AddForce(counterJumpForce * playerBody.mass);
            }
        }
    }
    public void NextDash()
    {
        nextDash = Time.time + dashCD;
    }

    public static float CalculateJumpForce(float gravityStrength, float jumpHeight)
    {
        //h = v^2/2g
        //2gh = v^2
        //sqrt(2gh) = v
        return Mathf.Sqrt(2 * gravityStrength * jumpHeight);
    }

    public void CreateDust()
    {
        dust.Play();
    }
}
