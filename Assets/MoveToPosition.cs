using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPosition : MonoBehaviour
{

    public GameObject currentObject;
    public Transform targetPosition;
    public float speed = 1.0f;
    public bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            currentObject.transform.position = Vector3.MoveTowards(currentObject.transform.position, targetPosition.position, speed * Time.deltaTime);
            if(currentObject.transform.position == targetPosition.position)
            {
                active = false;
            }
        }
        
    }

    public void Move()
    {
        active = true;
    }

    public void StopMove()
    {
        active = false;
    }
}
