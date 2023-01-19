using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Flying : StateMachineBehaviour
{
    Rigidbody2D rigidbody;
    Transform player;
    BossBehavior boss;
    Transform bossTransform;

    public float moveSpeed;

    public float attackCooldown;

    Vector2 flyPos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = animator.transform.parent.GetComponent<Rigidbody2D>();
        boss = animator.transform.GetComponent<BossBehavior>();
        bossTransform = animator.transform;

        // Pick random fly position
        flyPos = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));

        Debug.Log(flyPos.ToString());
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Code for boss flying
        boss.LookAtPlayer();

        Vector2 bossPos = bossTransform.position;
        bossTransform.position = Vector2.MoveTowards(bossPos, flyPos, moveSpeed * Time.deltaTime);

        /*Vector2 target = new Vector2(player.position.x, rigidbody.position.y);
        Vector2 newPos = Vector2.MoveTowards(rigidbody.position, target, moveSpeed * Time.fixedDeltaTime);
        rigidbody.MovePosition(newPos);*/

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

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
