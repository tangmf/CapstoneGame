using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Walking : StateMachineBehaviour
{
    Rigidbody2D rigidbody;
    Transform player;
    Boss1Behavior boss;

    public float moveSpeed = 0.8f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = animator.transform.parent.GetComponent<Rigidbody2D>();
        boss = animator.transform.parent.GetComponent<Boss1Behavior>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rigidbody.position.y);
        Vector2 newPos = Vector2.MoveTowards(rigidbody.position, target, moveSpeed * Time.fixedDeltaTime);
        rigidbody.MovePosition(newPos);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
