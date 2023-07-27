using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachObjectsToThisOnCollision : MonoBehaviour
{
    public Transform contents;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = contents;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        foreach (Transform go in contents)
        {
            go.transform.parent = null;
        }
    }
}
