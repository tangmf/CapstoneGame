using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public float nextSpawnTime = 0.0f;
    public float spawnCD = 5.0f;
    public int spawnCount = 1;
    public int maxSpawns = 5;
    public GameObject spawnPref;
    public Transform firepoint;
    public Transform container;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeSinceLevelLoad >= nextSpawnTime && CurrentSpawns() < maxSpawns )
        {
            for(int i=0; i < spawnCount; i++)
            {
                SpawnNow();
            }

            nextSpawnTime += spawnCD;
        }
    }

    public void SpawnNow()
    {
        GameObject newSpawn = Instantiate(spawnPref, firepoint.position, firepoint.rotation);
        newSpawn.transform.parent = container;
        
    }

    int CurrentSpawns()
    {
        int count = 0;
        foreach (Transform x in container)
        {
            count++;
        }
        return count;
    }
}
