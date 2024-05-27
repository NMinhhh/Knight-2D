using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy6 : BossEnemy
{
    [Header("Intrinsic")]
    [SerializeField] private GameObject enemyPref;
    [SerializeField] private float spawnEnemyCooldown;
    private float spawnEnemyTimer;
    [SerializeField] private GameObject signalObj;
    [SerializeField] private float signalTimer;

    private static int sortingOrder;

    [Header("Shooting bullet skill")]
    //Bullet pref
    [SerializeField] private GameObject bulletPref;
    private NormalBullet bulletScript;
    private GameObject bulletGo;
    [SerializeField] private Transform shootingPoint;

    //Amount of shoot
    [SerializeField] private int amountOfShoot;
    private int currentAmountOfShoot;

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
    private float angle;

    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        base.Update();

        base.Update();
        if (isCheckSkill)
            CheckCooldownSkill(selectedSkill);
        if (isReadySkills[0])
        {
            Reload();
            isMove = false;
            isShootingBulletSkill = true;
            ResetCooldownSkill(selectedSkill);
        }

        if (isShootingBulletSkill)
        {
            Shooting();
        }

        //Intrinsic
        spawnEnemyTimer += Time.deltaTime;
        if (spawnEnemyTimer > spawnEnemyCooldown)
        {
            spawnEnemyTimer = 0;
            StartCoroutine(SpawnEnemyMini());
        }

    }
    void Reload()
    {
        currentAmountOfShoot = amountOfShoot;
        amountOfBullet = (int)(Random.Range(amoutOfBulletRan.x, amoutOfBulletRan.y));
        angle = 180 / amountOfBullet;
    }

    void ShootingReset()
    {
        shootingPoint.localEulerAngles = new Vector3(0, 0, 0);
        amountOfBullet = (int)(Random.Range(amoutOfBulletRan.x, amoutOfBulletRan.y));
        currentAngle = 0;
        angle = 180 / amountOfBullet;
    }

    void Shooting()
    {
        if(currentAmountOfShoot > 0)
        {
            shootTimer -= Time.deltaTime;
            if(shootTimer <= 0) 
            {
                for (int i = 0; i < amountOfBullet; i++)
                {
                    SpawnBulletObj();
                }
                shootTimer = shootCooldown;
                currentAmountOfShoot--;
                ShootingReset();
            }
            
        }
        else
        {
            isShootingBulletSkill = false;
            ChangeSkillRandom();
            isMove = true;
        }
    }

    void SpawnBulletObj()
    {
        currentAngle += angle;
        shootingPoint.localEulerAngles = new Vector3(0, 0, currentAngle - 90 + transform.localEulerAngles.z);

        bulletGo = GameObject.Instantiate(bulletPref, transform.position, Quaternion.Euler(0, 0, shootingPoint.localEulerAngles.z));
        bulletScript = bulletGo.GetComponent<NormalBullet>();
        bulletScript.CreateBullet(bulletDamage, bulletSpeed, bulletTimeLife);
    }

    //=======================================Spaw enemy intrinsic===============================================

    IEnumerator SpawnEnemyMini()
    {
        Vector2 pos = GetPositionInCam.Instance.GetPositionInArea();
        GameObject go = Instantiate(signalObj, pos, Quaternion.identity);
        GameObject spawnObj = Instantiate(enemyPref, go.transform.position, Quaternion.identity);
        spawnObj.SetActive(false);
        spriteRenderer = spawnObj.GetComponent<SpriteRenderer>();
        sortingOrder++;
        spriteRenderer.sortingOrder = sortingOrder;
        yield return new WaitForSeconds(signalTimer);
        spawnObj.SetActive(true);
        Destroy(go);
    }
}
