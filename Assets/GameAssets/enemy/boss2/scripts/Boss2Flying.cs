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
    float attackCountdown;

    Vector2 flyPos;

    public Vector2 bottomLeftBounds = new Vector2(-18.0f,-3.0f);
    public Vector2 topRightBounds = new Vector2(18.0f, 7.0f);

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = animator.transform.parent.GetComponent<Rigidbody2D>();
        boss = animator.transform.GetComponent<BossBehavior>();
        bossTransform = animator.transform;

        attackCountdown = attackCooldown;

        // Pick random fly position
        flyPos = bossTransform.position;
        while (true)
        {
            if (Vector2.Distance(bossTransform.position, flyPos) < 15)
            {
                flyPos = new Vector2(Random.Range(bottomLeftBounds.x, topRightBounds.x), Random.Range(bottomLeftBounds.y, topRightBounds.y));
            }
            else
            {
                break;
            }
        }
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
        if (attackCountdown > 0)
        {
            attackCountdown -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("Boss_Attacking", true);
            animator.SetTrigger("Boss_Fire");
            attackCountdown = attackCooldown;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
