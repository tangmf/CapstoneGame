using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Firing : StateMachineBehaviour
{
    public float firingDuration;
    public float firingLoop;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        firingDuration = 3f;
        firingLoop = 1f;
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
        firingLoop -= Time.deltaTime;
        if (firingLoop > 0)
        {
            firingLoop -= Time.deltaTime;
        }
        else
        {
            
            firingLoop = 1f;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
