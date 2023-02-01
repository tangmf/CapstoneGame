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

    public float fireCooldown;
    float fireCountdown;
    public float laserCooldown;
    float laserCountdown;

    public bool shootBullet = false;
    public bool shootLaser = false;
    public bool finalPhase = false;

    bool firePattern1 = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = animator.transform.GetComponent<Rigidbody2D>();
        boss = animator.transform.GetComponent<BossBehavior>();
        bossTransform = animator.transform;

        fireCountdown = fireCooldown;
        firePattern1 = true;

        laserCountdown = laserCooldown;

        if (finalPhase)
        {
            boss.StartCrossLaser(2f, 1f);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // boss.LookAtPlayer();

        // Code for Firing Attack pattern
        if (shootBullet)
        {
            if (fireCountdown > 0)
            {
                fireCountdown -= Time.deltaTime;
            }
            else
            {
                if (firePattern1)
                {
                    boss.Fire(15, 24);
                    fireCountdown = fireCooldown;
                    firePattern1 = false;
                }
                else
                {
                    boss.Fire(16, 22.5f);
                    fireCountdown = fireCooldown;
                    firePattern1 = true;
                }
            }
        }

        if (shootLaser)
        {
            if (laserCountdown > 0)
            {
                laserCountdown -= Time.deltaTime;
            }
            else
            {
                boss.StartShootLaser(0.5f, 0.3f, 0.75f);
                laserCountdown = laserCooldown;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
