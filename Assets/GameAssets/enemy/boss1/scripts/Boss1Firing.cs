using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Firing : StateMachineBehaviour
{
    float firingDuration;
    float firingLoop;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        firingDuration = 3f;
        firingLoop = 0.5f;
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
            Fire();
            firingLoop = 0.5f;
        }
    }

    void Fire()
    {
        createBullet(0);
        createBullet(-20);
        createBullet(-40);
        createBullet(20);
        createBullet(40);

        void createBullet(float angle)
        {
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            GameObject player = GameObject.FindWithTag("Player");
            Vector2 playerPos = player.transform.position;
            Vector2 currPos = transform.position;
            Vector2 force = (playerPos - currPos).normalized;

            if (force.x > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (force.x < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            bullet.GetComponent<BulletBehaviour>().SetForce(RotateVector(force, angle));
            bullet.GetComponent<BulletBehaviour>().ignoreTag = gameObject.tag;
            bullet.GetComponent<BulletBehaviour>().damageTag = "Player";

            Destroy(bullet, 2f);
        }

        Vector2 RotateVector(Vector2 v, float angle)
        {
            float radian = angle * Mathf.Deg2Rad;
            float _x = v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian);
            float _y = v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian);
            return new Vector2(_x, _y);
        }
    }
}
