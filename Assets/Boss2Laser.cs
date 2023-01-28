using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Laser : MonoBehaviour
{
    Transform player;

    public string ignoreTag;

    public float laserLength;

    public LineRenderer lineRenderer;

    public GameObject hitbox;

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

        //playerPos[1] += 1f;
        Vector3 offshootPos = currentPos + ((playerPos - currentPos).normalized * laserLength);

        Vector3[] laserPath = new Vector3[] { currentPos, offshootPos };
        lineRenderer.SetPositions(laserPath);
    }

    public void LaserAttack(Vector3 playerPos, Vector3 currentPos, float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        //playerPos[1] += 1f;
        Vector3 offshootPos = currentPos + ((playerPos - currentPos).normalized * laserLength);

        Vector3[] laserPath = new Vector3[] { currentPos, offshootPos };
        lineRenderer.SetPositions(laserPath);

        // Code for hitbox
        Vector3 dir = lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0);
        float height = dir.magnitude;
        Vector3 dir2 = dir * 0.5f;
        Vector3 center = lineRenderer.GetPosition(0) + dir2;

        float rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        hitbox.transform.position = center;

        hitbox.transform.eulerAngles = new Vector3(0, 0, rotation);
        hitbox.transform.localScale = new Vector3(66.75f, width * 0.6675f, 0);
    }
}
