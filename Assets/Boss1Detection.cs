using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Detection : MonoBehaviour
{
    public Animator animator;

    public bool player_seen = true;

    // Start is called before the first frame update
    void Start()
    {
        player_seen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("SawPlayer");

            animator.SetBool("Player_Seen", true);
            player_seen = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("UnSawPlayer");
            
            animator.SetBool("Player_Seen", false);
            player_seen = false;
        }
    }
}
