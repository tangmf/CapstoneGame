using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4Crawling : StateMachineBehaviour
{
    Rigidbody2D rigidbody;
    Transform player;
    BossBehavior boss;
    Transform bossTransform;

    public GameObject boss4CrossLaser;

    public float moveSpeed;

    public float fireCooldown;
    float fireCountdown;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = animator.transform.parent.GetComponent<Rigidbody2D>();
        boss = animator.transform.GetComponent<BossBehavior>();
        bossTransform = animator.transform;

        fireCountdown = fireCooldown;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Code for boss crawling
        Vector2 currentPos = bossTransform.position;
        Vector2 playerPos = player.position;

        Vector2 direction = playerPos - currentPos;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bossTransform.eulerAngles = new Vector3(0, 0, rotation);

        bossTransform.position = Vector2.MoveTowards(currentPos, playerPos, moveSpeed * Time.deltaTime);

        // Code for boss firing
        if (fireCountdown > 0)
        {
            fireCountdown -= Time.deltaTime;
        }
        else
        {
            boss.Fire(36, 10);
            fireCountdown = fireCooldown;
        }

        // Code for boss laser
        boss4CrossLaser.transform.Rotate(new Vector3(0, 0, 0.25f));
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
