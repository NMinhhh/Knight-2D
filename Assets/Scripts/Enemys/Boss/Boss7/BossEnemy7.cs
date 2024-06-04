using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy7 : BossEnemy
{
    [Header("Shooting bullet skill")]
    //Bullet pref
    [SerializeField] private GameObject bulletPref;
    private NormalBullet bulletScript;
    private GameObject bulletGo;
    [SerializeField] private Transform[] shootingPoint;

    //Cooldown
    [SerializeField] private float shootCooldown;
    private float shootTimer;

    //Amount of bullets
    [SerializeField] private Vector2 amoutOfBulletRan;
    private int amountOfBullet;

    //Bullet info
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletTimeLife;

    private bool isShootingBulletSkill;
    //Angle to shoot
    private float currentAngle;
    [SerializeField] private Vector2 angleRan;
    private bool changeDir;


    [Header("Create bombexplode skill")]
    //bomb pref
    [SerializeField] private GameObject bombExplodePref;

    //bomb info
    [SerializeField] private float bombSpeed;
    [SerializeField] private float bulletBombDamage;
    [SerializeField] private Vector2 delayExplosion;

    //amount of bomb
    [SerializeField] private int amountOfBomb;
    private int currentAmountOfBomb;

    //amount of bullet in bomb
    [SerializeField] private int amountOfBulletInBomb;

    //Cooldown
    [SerializeField] private float createBombCooldown;
    private float createBombTimer;

    //Cool down Skill
    [SerializeField] private float createBombSkillCooldown;
    private float createBombSkillTimer;

    private BombExplode bombExplodeScript;
    private GameObject bombExplodeGo;

    [Header("Cave Summon Enemy Skill")]
    //Cave pref
    [SerializeField] private GameObject cavePref;
    [SerializeField] private int amountOfCave;
    //Enenmy in cave
    [SerializeField] private GameObject[] enemys;
    [SerializeField] private int amountOfEnemy;
    [SerializeField] private float summonCooldown;
    //Signal
    [SerializeField] private GameObject signalObj;
    [SerializeField] private float signalTimer;

    private CaveSummonEnenmy summonEnenmyScript;
    private GameObject caveGo;
    private bool isCaveSummonEnemySkill;

    private List<GameObject> enemies = new List<GameObject>();

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (isCheckSkill)
            CheckCooldownSkill(selectedSkill);
        if (isReadySkills[0])
        {
            isShootingBulletSkill = true;
            isMove = false;
            Reload();
            ResetCooldownSkill(selectedSkill);
        }
        else if (isReadySkills[1])
        {
            isMove = false;
            isCaveSummonEnemySkill = true;
            ResetCooldownSkill(selectedSkill);

        }

        if (isShootingBulletSkill)
        {
            Shooting();
        }
        else if (isCaveSummonEnemySkill)
        {
            isCaveSummonEnemySkill = false;
            StartCoroutine(SpawnCave());
        }

        createBombSkillTimer += Time.deltaTime;
        if (createBombSkillTimer > createBombSkillCooldown)
        {
            CreateBomb();
        }
    }

    //=============================shooting Skillll=====================

    void Reload()
    {
        amountOfBullet = (int)(Random.Range(amoutOfBulletRan.x, amoutOfBulletRan.y));
        currentAngle = 0;
        foreach (Transform point in shootingPoint)
        {
            point.localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    void Shooting()
    {
        if (amountOfBullet > 0)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                shootTimer = shootCooldown;
                amountOfBullet--;
                SpawnBulletObj();
            }
        }
        else
        {
            isShootingBulletSkill = false;
            isMove = true;
            ChangeSkillRandom();
        }


    }

    float GetAngle()
    {
        return Random.Range(angleRan.x, angleRan.y);
    }

    void SpawnBulletObj()
    {
        if (!changeDir)
        {
            currentAngle += GetAngle();
            if (currentAngle >= 120)
            {
                changeDir = true;
                currentAngle -= GetAngle();
            }
        }
        else
        {
            currentAngle -= GetAngle();
            if (currentAngle <= 0)
            {
                changeDir = false;
                currentAngle += GetAngle();
            }
        }
        foreach (Transform point in shootingPoint)
        {
            point.localEulerAngles = new Vector3(0, 0, currentAngle - 90 + transform.localEulerAngles.z);
            bulletGo = GameObject.Instantiate(bulletPref, point.transform.position, Quaternion.Euler((point == shootingPoint[0] ? 0 : 180), 0, point.localEulerAngles.z));
            bulletScript = bulletGo.GetComponent<NormalBullet>();
            bulletScript.CreateBullet(bulletDamage, bulletSpeed, bulletTimeLife);
        }
    }

    //=======================================Cave Summon Enemy Skill===============================================

    Vector3[] GetDestination()
    {
        Vector3[] destination = new Vector3[amountOfCave];
        for (int i = 0; i < amountOfCave; i++)
        {
            destination[i] = GetPositionInCam.Instance.GetPositionInArea();
        }
        return destination;
    }

    IEnumerator SpawnCave()
    {
        Vector3[] destination = GetDestination();
        for (int i = 0; i < destination.Length; i++)
        {
            GameObject go = Instantiate(signalObj, destination[i], Quaternion.identity);
            Destroy(go, signalTimer);
        }
        yield return new WaitForSeconds(signalTimer);
        for (int i = 0; i < amountOfCave; i++)
        {
            caveGo = Instantiate(cavePref, destination[i], Quaternion.identity);
            summonEnenmyScript = caveGo.GetComponent<CaveSummonEnenmy>();
            summonEnenmyScript.CreateCave(enemys, amountOfEnemy, summonCooldown);
        }
        isMove = true;
        ChangeSkillRandom();
    }

    void EnemyAllDeath()
    {
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Cave"));

        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i]);
        }
        enemies.Clear();

    }

    //=============================== Bomb ===========================
    void CreateBomb()
    {
        if (currentAmountOfBomb > 0)
        {
            createBombTimer -= Time.deltaTime;
            if (createBombTimer <= 0)
            {
                createBombTimer = createBombCooldown;
                SpawnBombObj();
                currentAmountOfBomb--;

            }
        }
        else
        {
            currentAmountOfBomb = amountOfBomb;
            createBombSkillTimer = 0;
        }
    }

    void SpawnBombObj()
    {
        Vector3 destination;
        destination = GetPositionInCam.Instance.GetPositionInArea();
        bombExplodeGo = Instantiate(bombExplodePref, transform.position, Quaternion.identity);
        bombExplodeScript = bombExplodeGo.GetComponent<BombExplode>();
        bombExplodeScript.CreateBomb(destination, bombSpeed, Random.Range(delayExplosion.x, delayExplosion.y), bulletBombDamage, amountOfBulletInBomb);
    }

    protected override void Dead()
    {
        base.Dead();
        EnemyAllDeath();
    }
}
