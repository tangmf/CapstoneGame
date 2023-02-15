using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public Transform target;
    GameObject player;
    public float moveSpeed = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        this.transform.position = Vector2.MoveTowards(this.transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    public void LookAtPlayer()
    {

        if (transform.position.x < player.transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
