using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public float walkSpeed = 15f;
    public float jumpSpeed = 10f;
    public GameObject groundRay;
    public float jumpFloorDistance = 0.2f;
    public LayerMask layerMask;
    Rigidbody2D rb;
    Animator animator;
    public bool grounded;
    public bool walkDir = true;

    public enum State
    {
        Walking,
        Idling,
        Jump,
        Interact
    }

    public State currentState = State.Idling;

    public float decisionTime = 3.0f;
    public float nextTime = 0f;
    public float idleTime = 0f;
    public float maxIdleTime = 15f;

    public bool freeze = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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

        if(currentState == State.Walking)
        {
            Walk(walkDir);
            animator.SetBool("Moving", true);

            if (walkDir)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else
        {
            // idle
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetBool("Moving", false);
            idleTime += Time.deltaTime;
            if(Time.timeSinceLevelLoad > nextTime && !freeze)
            {
                currentState = State.Walking;
                nextTime = Time.timeSinceLevelLoad + decisionTime;
            }
        }
        if(idleTime >= maxIdleTime && !freeze)
        {
            idleTime = 0;
            currentState = State.Walking;
        }


    }

    public void Jump()
    {
        rb.velocity = Vector2.up * jumpSpeed * Time.deltaTime;
    }

    public void Walk(bool b)
    {
        if (b)
        {
            rb.velocity = new Vector2(walkSpeed * Time.deltaTime, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-walkSpeed * Time.deltaTime, rb.velocity.y);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentState != State.Idling)
        {
            if (collision.gameObject.GetComponent<Interactable>())
            {
                collision.gameObject.GetComponent<Interactable>().Interact(this.gameObject);
                SetToIdle();
            }
        }
        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(currentState == State.Walking)
        {
            walkDir = !walkDir;
        }
        else
        {
            Debug.Log("Interrupted");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    public void SetToIdle()
    {
        currentState = State.Idling;
        nextTime = Time.timeSinceLevelLoad + decisionTime;
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);

    }

    public void Freeze()
    {
        SetToIdle();
        freeze = true;
    }

    public void UnFreeze()
    {
        freeze = false;
        currentState = State.Walking;
    }

    public IEnumerator UnFreezeInSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        UnFreeze();
    }

    public void CallUnFreezeInSeconds(float time)
    {
        Debug.Log("Unfrozen");
        StartCoroutine(UnFreezeInSeconds(time));
    }
}
