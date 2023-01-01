using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Detection : MonoBehaviour
{
    public Animator animator;

    bool player_seen;
    bool boss_attacking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Player_Seen", true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Player_Seen", false);
            animator.SetBool("Boss_Attacking", false);
        }
    }
}
