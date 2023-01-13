using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Firing : StateMachineBehaviour
{
    Rigidbody2D rigidbody;
    Transform player;
    BossBehavior boss;

    public float firingDuration = 3f;
    public float firingLoop = 1f;
    public bool shootBullet = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = animator.transform.parent.GetComponent<Rigidbody2D>();
        boss = animator.transform.parent.GetComponent<BossBehavior>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Code for counting down to Firing Attack
        if (firingDuration > 0)
        {
            firingDuration -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("Boss_Attacking", false);
            firingDuration = 3f;
        }

        // Code for Firing Attack pattern
        if (firingLoop > 0)
        {
            firingLoop -= Time.deltaTime;
        }
        else
        {
            if (shootBullet)
            {
                boss.Fire(5, 3);
            }
            // For second attack in the future
            /*if (shootSpike)
            {
                boss.StartShootSpike();
            }*/
            
            firingLoop = 1f;
        }
    }
}
