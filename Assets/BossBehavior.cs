using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    Rigidbody2D body;
    public Animator animator;
    Collider2D hitbox;

    Transform player;

    public GameObject eyeBulletPrefab;
    public Transform firePoint;
    public Transform lowFirePoint;
    public Transform highFirePoint;

    public GameObject warning;
    public GameObject spike;
    public GameObject platforms;

    public bool isFlipped = false;

    public HealthManager healthManager;
    public Boss2Laser boss2Laser;
    public LaserHitbox laserHitbox;
    public Boss4CrossLaser boss4CrossLaser;

    public float telegraphDuration;
    public float delayDuration;
    public float attackDuration;

    public bool enraged1;
    public bool enraged2;

    public float enrageLevel1;
    public float enrageLevel2;
    public bool thirdPhase;

    public float attackRange = 3.0f;
    public float meleeDmg = 10f;

    public List<GameObject> platformsList;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<Collider2D>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().pivot;

        //Physics2D.IgnoreCollision(hitbox, player.GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        if(!enraged1 && healthManager.healthPoints <= healthManager.healthBar.maxValue * enrageLevel1)
        {
            animator.SetTrigger("Enrage1");
            enraged1 = true;
        }
        else if (!enraged2 && healthManager.healthPoints <= healthManager.healthBar.maxValue * enrageLevel2)
        {
            animator.SetTrigger("Enrage2");
            enraged2 = true;
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void Fire(int count, float spreadAngle)
    {
        if (count % 2 == 0) // If projectile count even
        {
            for (int i = 1; i <= count / 2; i++)
            {
                createEyeBullet((i * spreadAngle) - spreadAngle / 2);
                createEyeBullet(-((i * spreadAngle) - spreadAngle / 2));
            }
        }
        else // If projectile count odd
        {
            createEyeBullet(0);

            for (int i = 1; i <= (count - 1) / 2; i++)
            {
                createEyeBullet(i * spreadAngle);
                createEyeBullet(-i * spreadAngle);
            }
        }
    }

    public void Fire2(int count)
    {
        float spreadAngle = 10.0f;
        if (count % 2 == 0) // If projectile count even
        {
            for (int i = 1; i <= count / 2; i++)
            {
                createEyeBullet((i * spreadAngle) - spreadAngle / 2);
                createEyeBullet(-((i * spreadAngle) - spreadAngle / 2));
            }
        }
        else // If projectile count odd
        {
            createEyeBullet(0);

            for (int i = 1; i <= (count - 1) / 2; i++)
            {
                createEyeBullet(i * spreadAngle);
                createEyeBullet(-i * spreadAngle);
            }
        }
    }

    public void StartShootSpike()
    {
        StartCoroutine(ShootSpike());
    }

    IEnumerator ShootSpike()
    {
        var targetPos = player.position;
        targetPos.y = -10;
        GameObject newWarning = Instantiate(warning, targetPos, Quaternion.identity);
        yield return new WaitForSeconds(1.0f);
        GameObject newSpike = Instantiate(spike, targetPos, Quaternion.identity);
        newSpike.GetComponent<SpikeBehaviour>().ignoreTag = gameObject.tag;
        newSpike.GetComponent<SpikeBehaviour>().damageTag = "Player";
    }

    public void ShootSpikeInstant()
    {
        var targetPos = player.position;
        targetPos.y = -10;
        GameObject newSpike = Instantiate(spike, targetPos, Quaternion.identity);
        newSpike.GetComponent<SpikeBehaviour>().ignoreTag = gameObject.tag;
        newSpike.GetComponent<SpikeBehaviour>().damageTag = "Player";
    }

    void createEyeBullet(float angle)
    {
        Vector2 playerPos = player.position;
        Vector2 currentPos = firePoint.position;

        Vector2 force = (Quaternion.AngleAxis(angle, Vector3.forward) * (playerPos - currentPos)).normalized;

        // To rotate bullet sprite
        float rotation = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;

        // Create Bullet
        GameObject newBullet = Instantiate(eyeBulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, rotation));
        newBullet.GetComponent<BulletBehaviour>().SetForce(force);
        newBullet.GetComponent<BulletBehaviour>().ignoreTag = gameObject.tag;
        newBullet.GetComponent<BulletBehaviour>().damageTag = "Player";

        // Destroy after 5 seconds
        Destroy(newBullet, 5f);


        /*
        var newRotation = firePoint.rotation;
        newRotation *= Quaternion.Euler(0, 0, -90 + angle);
        GameObject eyeBullet = Instantiate(eyeBulletPrefab, firePoint.position, newRotation);

        Vector2 playerPos = player.transform.position;
        Vector2 currPos = transform.position;
        Vector2 force = (playerPos - currPos).normalized;

        eyeBullet.GetComponent<BulletBehaviour>().SetForce(RotateVector(force, angle));
        eyeBullet.GetComponent<BulletBehaviour>().ignoreTag = gameObject.tag;
        eyeBullet.GetComponent<BulletBehaviour>().damageTag = "Player";

        Destroy(eyeBullet, 2f);
        */
    }

    public void ShootHorizontal()
    {

        Vector2 playerPos = player.position;
        Vector2 currentPos = firePoint.position;

        Vector2 force = (playerPos - currentPos).normalized;

        force.y = 0;
        if (force.x >= 0)
        {
            force.x = 1;
        }
        else
        {
            force.x = -1;
        }

        // To rotate bullet sprite
        float rotation = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;

        // Create Bullet
        GameObject newBullet = Instantiate(eyeBulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, rotation));
        newBullet.GetComponent<BulletBehaviour>().SetForce(force);
        newBullet.GetComponent<BulletBehaviour>().ignoreTag = gameObject.tag;
        newBullet.GetComponent<BulletBehaviour>().damageTag = "Player";

        // Destroy after 5 seconds
        Destroy(newBullet, 5f);
    }

    Vector2 RotateVector(Vector2 v, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float _x = v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian);
        float _y = v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian);
        return new Vector2(_x, _y);
    }

    public void StartShootLaser(float telegraphDuration, float delayDuration, float attackDuration)
    {
        StartCoroutine(ShootLaser(telegraphDuration, delayDuration, attackDuration));
    }

    IEnumerator ShootLaser(float telegraphDuration, float delayDuration, float attackDuration)
    {
        Vector2 playerPos = player.position;
        Vector2 currentPos = transform.position;
        float width = 0.1f;

        boss2Laser.ShowLaser();

        while (telegraphDuration > 0)
        {
            playerPos = player.position;
            currentPos = transform.position;
            boss2Laser.LaserTelegraph(playerPos, currentPos, width);
            telegraphDuration -= Time.deltaTime;
            yield return null;
        }
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
            boss2Laser.LaserAttack(playerPos, currentPos, width);
            attackDuration -= Time.deltaTime;
            yield return null;
        }
        while (width > 0)
        {
            width = width - 0.1f;
            boss2Laser.LaserAttack(playerPos, currentPos, width);
            yield return null;
        }

        laserHitbox.laserDamaging = false;
        boss2Laser.HideLaser();

        animator.SetBool("Boss_Attacking", false);
    }

    public void ShootLow()
    {
        StartCoroutine(ChangeFirepointLow());
    }

    IEnumerator ChangeFirepointLow()
    {
        Transform normalFirepoint = firePoint;
        firePoint = lowFirePoint;
        ShootHorizontal();
        yield return new WaitForSeconds(0.1f);
        firePoint = normalFirepoint;
    }


    public void ShootHigh()
    {
        StartCoroutine(ChangeFirepointHigh());
    }

    IEnumerator ChangeFirepointHigh()
    {
        Transform normalFirepoint = firePoint;
        firePoint = highFirePoint;
        ShootHorizontal();
        yield return new WaitForSeconds(0.1f);
        firePoint = normalFirepoint;
    }

    public void RandomAttack1()
    {
        var rand = Random.Range(0f, 1.0f);
        if(rand >= 0.5f)
        {
            ShootHigh();
        }
        else
        {
            ShootLow();
        }
    }

    public void StartCrossLaser(float telegraphDuration, float delayDuration)
    {
        StartCoroutine(CrossLaser(telegraphDuration, delayDuration));
    }

    IEnumerator CrossLaser(float telegraphDuration, float delayDuration)
    {
        Vector2 currentPos = transform.position;
        float width = 0.1f;

        boss4CrossLaser.ShowLaser();

        while (telegraphDuration > 0)
        {
            currentPos = transform.position;
            boss4CrossLaser.LaserTelegraph(currentPos, width);
            telegraphDuration -= Time.deltaTime;

            yield return null;
        }

        while (delayDuration > 0)
        {
            if (width < 3)
            {
                width = width + 0.1f;
            }
            boss4CrossLaser.LaserAttack(currentPos, width);
            delayDuration -= Time.deltaTime;
            yield return null;
        }

        animator.SetTrigger("FinalPhase");
    }

    public void StartShootBoss4Spike()
    {
        StartCoroutine(ShootBoss4Spike());
    }

    IEnumerator ShootBoss4Spike()
    {
        // Code to position spike
        var playerPos = player.position;
        GameObject spikePlatform = platforms.transform.GetChild(0).GetChild(0).gameObject;

        foreach (Transform platformRow in platforms.transform)
        {
            foreach (Transform platformGroup in platformRow.transform)
            {
                platformsList.Add(platformGroup.gameObject);

                float closestDistance = Vector2.Distance(playerPos, spikePlatform.transform.position);
                float platformDistance = Vector2.Distance(playerPos, platformGroup.position);

                if (platformDistance < closestDistance)
                {
                    spikePlatform = platformGroup.gameObject;
                }
            }
        }

        // Code to spawn spike
        Vector3 spikePlatformSpawn1 = spikePlatform.transform.position;
        spikePlatformSpawn1.y += 0.2f;
        Vector3 spikePlatformSpawn2 = spikePlatform.transform.position;
        spikePlatformSpawn2.x += 2;
        spikePlatformSpawn2.y -= 0.3f;
        Vector3 spikePlatformSpawn3 = spikePlatform.transform.position;
        spikePlatformSpawn3.x += -2;
        spikePlatformSpawn3.y -= 0.3f;
        Vector3 spikePlatformSpawn4 = spikePlatform.transform.position;
        spikePlatformSpawn4.x += 4;
        spikePlatformSpawn4.y -= 1f;
        Vector3 spikePlatformSpawn5 = spikePlatform.transform.position;
        spikePlatformSpawn5.x += -4;
        spikePlatformSpawn5.y -= 1f;

        ShootBoss4SpikeWarning(spikePlatformSpawn1);
        ShootBoss4SpikeWarning(spikePlatformSpawn2);
        ShootBoss4SpikeWarning(spikePlatformSpawn3);
        ShootBoss4SpikeWarning(spikePlatformSpawn4);
        ShootBoss4SpikeWarning(spikePlatformSpawn5);

        yield return new WaitForSeconds(1.0f);

        ShootBoss4SpikeAttack(spikePlatformSpawn1);
        ShootBoss4SpikeAttack(spikePlatformSpawn2);
        ShootBoss4SpikeAttack(spikePlatformSpawn3);
        ShootBoss4SpikeAttack(spikePlatformSpawn4);
        ShootBoss4SpikeAttack(spikePlatformSpawn5);
    }

    public void ShootBoss4SpikeWarning(Vector3 spikePlatformSpawn)
    {
        GameObject newWarning = Instantiate(warning, spikePlatformSpawn, Quaternion.identity);
    }

    public void ShootBoss4SpikeAttack(Vector3 spikePlatformSpawn)
    {
        GameObject newSpike = Instantiate(spike, spikePlatformSpawn, Quaternion.identity);

        newSpike.GetComponent<SpikeBehaviour>().ignoreTag = gameObject.tag;
        newSpike.GetComponent<SpikeBehaviour>().damageTag = "Player";
    }

    public void MeleeAtk()
    {
        Vector2 playerPos = player.position;
        Vector2 currentPos = firePoint.position;

        Vector2 force = (playerPos - currentPos).normalized;
        float rotation = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(firePoint.position, attackRange);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (!enemy.CompareTag("Ground"))
            {
                if (enemy.CompareTag("Player") && enemy.gameObject.GetComponent<HealthManager>())
                {
                    enemy.gameObject.GetComponent<HealthManager>().Damage(meleeDmg);
                }

                if (enemy.gameObject.GetComponent<BulletBehaviour>() && enemy.gameObject.GetComponent<Rigidbody2D>())
                {
                    enemy.transform.position = firePoint.position;
                    //force.y += prevBulletTransform.rotation.eulerAngles.z;
                    enemy.gameObject.GetComponent<BulletBehaviour>().SetForce(force * 2);
                    //enemy.gameObject.GetComponent<Rigidbody2D>().AddForce(force * 1.0f, ForceMode2D.Impulse);
                    enemy.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
                    enemy.gameObject.GetComponent<BulletBehaviour>().ignoreTag = gameObject.tag;
                    enemy.gameObject.GetComponent<BulletBehaviour>().damageTag = "Player";
                }

                if (enemy.gameObject.GetComponent<Rigidbody2D>() && !enemy.gameObject.GetComponent<BulletBehaviour>())
                {

                    enemy.gameObject.GetComponent<Rigidbody2D>().AddForce(force * 50.0f, ForceMode2D.Impulse);
                }
            }



        }
    }
}
