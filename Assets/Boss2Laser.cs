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

    public void LaserTelegraph(Vector3 playerPos, Vector3 currentPos, float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.startColor = new Color(0.5f, 0, 0);
        lineRenderer.endColor = new Color(0.5f, 0, 0);

        playerPos[1] += 1f;
        Vector3 offshootPos = (playerPos - currentPos) * laserLength;

        Vector3[] laserPath = new Vector3[] { currentPos, offshootPos };
        lineRenderer.SetPositions(laserPath);
    }

    public void LaserAttack(Vector3 playerPos, Vector3 currentPos, float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        playerPos[1] += 1.75f;
        Vector3 offshootPos = (playerPos - currentPos) * laserLength;

        Vector3[] laserPath = new Vector3[] { currentPos, offshootPos };
        lineRenderer.SetPositions(laserPath);
    }
}
