using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToPositionOnStart : MonoBehaviour
{
    public Transform targetTransform;
    public Transform targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        targetTransform.position = targetPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
