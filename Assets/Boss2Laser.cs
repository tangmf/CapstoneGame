using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Laser : MonoBehaviour
{
    Transform player;

    public float laserLength;
    public LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLaser()
    {
        lineRenderer.enabled = true;
    }

    public void HideLaser()
    {
        lineRenderer.enabled = false;
    }

    public void LaserTelegraph(Vector3 playerPos, Vector3 currentPos)
    {
        lineRenderer.startWidth = 0.1f;

        playerPos[1] += 1.75f;
        Vector3 offshootPos = (playerPos - currentPos) * laserLength;

        Vector3[] laserPath = new Vector3[] { currentPos, offshootPos };
        lineRenderer.SetPositions(laserPath);
    }

    public void LaserAttack(Vector3 playerPos, Vector3 currentPos)
    {
        lineRenderer.startWidth = 1f;

        playerPos[1] += 1.75f;
        Vector3 offshootPos = (playerPos - currentPos) * laserLength;

        Vector3[] laserPath = new Vector3[] { currentPos, offshootPos };
        lineRenderer.SetPositions(laserPath);
    }
}
