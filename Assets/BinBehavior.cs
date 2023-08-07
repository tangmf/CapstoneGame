using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinBehavior : MonoBehaviour
{
    public float cd = 1.0f;
    public float next = 0f;
    public GameObject trashPref;
    public Transform spawnPoint;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.x != 0 && Time.timeSinceLevelLoad > next){
            GameObject trash = Instantiate(trashPref, spawnPoint.position, Quaternion.identity);
            Destroy(trash, 3.0f);
            next = Time.timeSinceLevelLoad + cd;
        }
    }
}
