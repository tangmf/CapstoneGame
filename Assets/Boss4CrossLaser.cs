using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4CrossLaser : MonoBehaviour
{
    public string ignoreTag;

    public float laserLength;

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
        LaserTelegraphI(currentPos, width, boss4CrossLaser1, lineRenderer1, 0);
        LaserTelegraphI(currentPos, width, boss4CrossLaser2, lineRenderer2, 90);
        LaserTelegraphI(currentPos, width, boss4CrossLaser3, lineRenderer3, 180);
        LaserTelegraphI(currentPos, width, boss4CrossLaser4, lineRenderer4, 270);
    }

    public void LaserTelegraphI(Vector3 currentPos, float width, GameObject boss4CrossLaser, LineRenderer lineRenderer, float angle)
    {
        // Code for LineRender
        lineRenderer.startWidth = width;
        lineRenderer.startColor = new Color(0.5f, 0, 0);
        lineRenderer.endColor = new Color(0.5f, 0, 0);

        Vector3 endPos = currentPos;
        endPos.x =+ laserLength;
        boss4CrossLaser.transform.eulerAngles = new Vector3(0, 0, angle);
        Vector3[] laserPath = new Vector3[] { currentPos, endPos };
        lineRenderer.SetPositions(laserPath);
    }

    public void LaserAttack(Vector3 playerPos, Vector3 currentPos, float width)
    {
        LaserAttackI(playerPos, currentPos, width, boss4CrossLaser1, lineRenderer1, hitbox1, 0);
        LaserAttackI(playerPos, currentPos, width, boss4CrossLaser2, lineRenderer2, hitbox2, 90);
        LaserAttackI(playerPos, currentPos, width, boss4CrossLaser3, lineRenderer3, hitbox3, 180);
        LaserAttackI(playerPos, currentPos, width, boss4CrossLaser4, lineRenderer4, hitbox4, 270);
    }

    public void LaserAttackI(Vector3 playerPos, Vector3 currentPos, float width, GameObject boss4CrossLaser, LineRenderer lineRenderer, GameObject hitbox, float angle)
    {
        // Code for LineRender
        lineRenderer.startWidth = width;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        Vector3 endPos = currentPos;
        endPos.x = +laserLength;
        boss4CrossLaser.transform.eulerAngles = new Vector3(0, 0, angle);
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
        hitbox.transform.localScale = new Vector3(66.75f, width * 0.6675f, 0);
    }
}
