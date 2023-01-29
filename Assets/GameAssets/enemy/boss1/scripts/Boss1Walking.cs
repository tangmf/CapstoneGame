using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Walking : StateMachineBehaviour
{
    Rigidbody2D rigidbody;
    Transform player;
    BossBehavior boss;

    public float moveSpeed;

    public float attackCooldown;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        rigidbody = animator.transform.GetComponent<Rigidbody2D>();
        boss = animator.transform.GetComponent<BossBehavior>();


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Code for boss walking
        boss.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rigidbody.position.y);
        boss.transform.position = Vector2.MoveTowards(rigidbody.position, target, moveSpeed * Time.deltaTime);
        //Vector2 newPos = Vector2.MoveTowards(rigidbody.position, target, moveSpeed * Time.fixedDeltaTime);
        //rigidbody.MovePosition(newPos);

        // Code for counting down to Firing Attack
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("Boss_Attacking", true);
            animator.SetTrigger("Boss_Fire");
            attackCooldown = 3f;
        }
    }

    
}
