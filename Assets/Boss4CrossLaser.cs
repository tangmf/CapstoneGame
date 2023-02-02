using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4CrossLaser : MonoBehaviour
{
    public string ignoreTag;

    public float laserLength;

    public GameObject boss;
    public GameObject boss4CrossLaser1;
    public GameObject boss4CrossLaser2;
    public GameObject boss4CrossLaser3;
    public GameObject boss4CrossLaser4;
    LineRenderer lineRenderer1;
    LineRenderer lineRenderer2;
    LineRenderer lineRenderer3;
    LineRenderer lineRenderer4;
    GameObject hitbox1;
    GameObject hitbox2;
    GameObject hitbox3;
    GameObject hitbox4;
    public GameObject boss4CrossLaserRotation;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer1 = boss4CrossLaser1.GetComponent<LineRenderer>();
        lineRenderer2 = boss4CrossLaser2.GetComponent<LineRenderer>();
        lineRenderer3 = boss4CrossLaser3.GetComponent<LineRenderer>();
        lineRenderer4 = boss4CrossLaser4.GetComponent<LineRenderer>();
        hitbox1 = boss4CrossLaser1.transform.GetChild(0).gameObject;
        hitbox2 = boss4CrossLaser2.transform.GetChild(0).gameObject;
        hitbox3 = boss4CrossLaser3.transform.GetChild(0).gameObject;
        hitbox4 = boss4CrossLaser4.transform.GetChild(0).gameObject;

        lineRenderer1.enabled = false;
        lineRenderer2.enabled = false;
        lineRenderer3.enabled = false;
        lineRenderer4.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLaser()
    {
        lineRenderer1.enabled = true;
        lineRenderer2.enabled = true;
        lineRenderer3.enabled = true;
        lineRenderer4.enabled = true;
    }

    public void HideLaser()
    {
        lineRenderer1.enabled = false;
        lineRenderer2.enabled = false;
        lineRenderer3.enabled = false;
        lineRenderer4.enabled = false;
    }

    public void LaserTelegraph(Vector3 currentPos, float width)
    {
        LaserTelegraphI(currentPos, width, lineRenderer1, boss4CrossLaser1.transform.up);
        LaserTelegraphI(currentPos, width, lineRenderer2, boss4CrossLaser1.transform.right);
        LaserTelegraphI(currentPos, width, lineRenderer3, -boss4CrossLaser1.transform.up);
        LaserTelegraphI(currentPos, width, lineRenderer4, -boss4CrossLaser1.transform.right);
    }

    public void LaserTelegraphI(Vector3 currentPos, float width, LineRenderer lineRenderer, Vector3 direction)
    {
        // Code for LineRender
        lineRenderer.startWidth = width;
        lineRenderer.startColor = new Color(0.5f, 0, 0);
        lineRenderer.endColor = new Color(0.5f, 0, 0);

        Vector3 endPos = currentPos + (direction * laserLength);
        Vector3[] laserPath = new Vector3[] { currentPos, endPos };
        lineRenderer.SetPositions(laserPath);
    }

    public void LaserAttack(Vector3 currentPos, float width)
    {
        LaserAttackI(currentPos, width, lineRenderer1, hitbox1, boss4CrossLaser1.transform.up);
        LaserAttackI(currentPos, width, lineRenderer2, hitbox2, boss4CrossLaser1.transform.right);
        LaserAttackI(currentPos, width, lineRenderer3, hitbox3, -boss4CrossLaser1.transform.up);
        LaserAttackI(currentPos, width, lineRenderer4, hitbox4, -boss4CrossLaser1.transform.right);
    }

    public void LaserAttackI(Vector3 currentPos, float width, LineRenderer lineRenderer, GameObject hitbox, Vector3 direction)
    {
        hitbox.GetComponent<LaserHitbox>().laserDamaging = true;

        // Code for LineRender
        lineRenderer.startWidth = width;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        Vector3 endPos = currentPos + (direction * laserLength);
        Vector3[] laserPath = new Vector3[] { currentPos, endPos };
        lineRenderer.SetPositions(laserPath);

        // Code for Hitbox
        Vector3 dir = lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0);
        //float height = dir.magnitude;
        Vector3 dir2 = dir * 0.5f;
        Vector3 center = lineRenderer.GetPosition(0) + dir2;

        float rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        hitbox.transform.position = center;

        hitbox.transform.eulerAngles = new Vector3(0, 0, rotation);

        float laserScale = 1 / boss.transform.lossyScale.x;
        hitbox.transform.localScale = new Vector3(laserLength * laserScale, width * laserScale, 0);
    }

    public void Rotate()
    {
        boss4CrossLaserRotation.transform.Rotate(new Vector3(0, 0, 0.20f));
    }
}
