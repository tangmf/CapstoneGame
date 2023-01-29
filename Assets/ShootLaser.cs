using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public float telegraphDuration = 1f;
    public float delayDuration = 0.5f;
    public float attackDuration = 1f;
    public Boss2Laser boss2Laser;
    public Vector2 targetPos;
    public LaserHitbox laserHitbox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootLaserDown(float y) {
        StartShootLaser(new Vector2(transform.position.x, transform.position.y + y));
    }

    public void StartShootLaser(Vector2 targetPos)
    {
        StartCoroutine(ShootLaserEnumerator(targetPos));
    }

    IEnumerator ShootLaserEnumerator(Vector2 targetPos)
    {
        Vector2 currentPos = transform.position;
        telegraphDuration = 1f;
        delayDuration = 0.5f;
        attackDuration = 1f;
        float width = 0.1f;

        boss2Laser.ShowLaser();
        // Use this later for attack
        //Physics2D.Raycast(currentPos, playerPos.normalized);
        laserHitbox.laserDamaging = false;
        while (telegraphDuration > 0)
        {
            currentPos = transform.position;
            boss2Laser.LaserTelegraph(targetPos, currentPos, width);
            telegraphDuration -= Time.deltaTime;
            yield return null;
        }
        laserHitbox.laserDamaging = true;
        while (delayDuration > 0)
        {
            delayDuration -= Time.deltaTime;
            yield return null;
        }

        laserHitbox.laserDamaging = true;

        while (attackDuration > 0)
        {
            if (width < 4)
            {
                width = width + 0.1f;
            }
            boss2Laser.LaserAttack(targetPos, currentPos, width);
            attackDuration -= Time.deltaTime;
            yield return null;
        }
        while (width > 0)
        {
            width = width - 0.1f;
            boss2Laser.LaserAttack(targetPos, currentPos, width);
            yield return null;
        }

        laserHitbox.laserDamaging = false;
        boss2Laser.HideLaser();
    }
}
