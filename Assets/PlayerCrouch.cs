using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : StateMachineBehaviour
{
    CapsuleCollider2D collider;
    Vector2 normalSize;
    Vector2 normalOffset;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        collider = player.GetComponent<CapsuleCollider2D>();
        normalSize = collider.size;
        var newSize = collider.size * new Vector2(1f, 0.5f);
        collider.size = newSize;

        normalOffset = collider.offset;
        var newOffset = collider.offset * new Vector2(1f, 0.5f);
        collider.offset = newOffset;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        collider.offset = normalOffset;
        collider.size = normalSize;
    }


    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }


    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }
}
