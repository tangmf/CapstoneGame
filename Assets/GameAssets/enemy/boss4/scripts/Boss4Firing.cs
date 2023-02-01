using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4Firing : StateMachineBehaviour
{
    Rigidbody2D rigidbody;
    Transform player;
    BossBehavior boss;
    Transform bossTransform;

    public float moveSpeed;

    public float laserDelay = 1.5f;
    public float laserDuration = 1f;

    public float fireCooldown;

    public bool shootBullet = false;
    public bool shootLaser = false;
    public bool shootCrossLaser = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = animator.transform.GetComponent<Rigidbody2D>();
        boss = animator.transform.GetComponent<BossBehavior>();
        bossTransform = animator.transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // boss.LookAtPlayer();
         
        // Code for Firing Attack pattern
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }
        else
        {
            if (shootBullet)
            {
                boss.Fire(3, 90);
            }

            fireCooldown = 1.5f;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
