using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarMachineBehavior : MonoBehaviour
{
    public Animator animator;
    Rigidbody2D warMachineBody;

    public float moveSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        warMachineBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
