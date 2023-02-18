using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public float nextSpawnTime = 0.0f;
    public float spawnCD = 5.0f;
    public float spawnCount = 1f;
    public float maxSpawns = 5f;
    Transform container;
    public GameObject spawnPref;
    public Transform firepoint;
    public bool scaleStrength = false;
    public float hpIncrease = 0f;
    public float hpScale = 5f;
    public float spawnScale = 1;
    public float scaleCD = 30f;
    public float maxSpawnScale = 1;
    public float nextScaleTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        container = new GameObject("spawncontainer").transform;
        //container = GameObject.FindGameObjectWithTag("SpawnContainer").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextScaleTime)
        {
            hpIncrease += hpScale;
            spawnCount += spawnScale;
            maxSpawns += maxSpawnScale;
            nextScaleTime += scaleCD;

            if(spawnCount >= 3)
            {
                spawnCount = 3;
            }
            if(maxSpawns >= 6)
            {
                maxSpawns = 6;
            }
        }
        if(Time.timeSinceLevelLoad >= nextSpawnTime && CurrentSpawns() < (int)maxSpawns )
        {
            for(int i=0; i < (int)spawnCount; i++)
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
        if (scaleStrength == true)
        {
            if (newSpawn.GetComponent<HealthManager>() != null)
            {
                newSpawn.GetComponent<HealthManager>().healthPoints += hpIncrease;
                newSpawn.GetComponent<HealthManager>().MaxHealth();
                Debug.Log("Updated hp");
            } 
        }
        
        
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
