using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4Crawling : StateMachineBehaviour
{
    Rigidbody2D rigidbody;
    Transform player;
    BossBehavior boss;
    Transform bossTransform;

    public float moveSpeed;

    bool isCrawling;



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = animator.transform.parent.GetComponent<Rigidbody2D>();
        boss = animator.transform.GetComponent<BossBehavior>();
        bossTransform = animator.transform;

        isCrawling = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Code for boss crawling
        bossTransform.LookAt(player);
        bossTransform.position = Vector2.MoveTowards(bossTransform.position, player.position, moveSpeed * Time.deltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
