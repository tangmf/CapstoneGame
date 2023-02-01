using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Firing : StateMachineBehaviour
{
    Rigidbody2D rigidbody;
    Transform player;
    BossBehavior boss;

    float exitTime = 0.0f;
    float firingLoop = 0.7f;
    public float stateDuration = 21.0f;
    public bool shootBullet = false;

    public float laserDelay = 1.5f;
    public float laserDuration = 1f;
    public bool shootLaser = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = animator.transform.GetComponent<Rigidbody2D>();
        boss = animator.transform.GetComponent<BossBehavior>();
        exitTime = Time.time + stateDuration;
        if (shootLaser)
        {
            boss.StartShootLaser(1f, 0.5f, 1f);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        // Code for counting down to Firing Attack
        //if (firingDuration > 0)
        if (Time.time < exitTime)
        {
            //firingDuration -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("Boss_Attacking", false);
            exitTime = 0.0f;
        }

        /*
         
        // Code for Firing Attack pattern
        if (firingLoop > 0)
        {
            firingLoop -= Time.deltaTime;
        }
        else
        {
            if (shootBullet)
            {
                boss.Fire(5, 20);
            }


            firingLoop = 0.7f;
        }
        */

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
