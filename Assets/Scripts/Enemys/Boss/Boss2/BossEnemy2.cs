using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BossEnemy2 : BossEnemy
{
    [Header("Shooting Bullet Skill")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private int amountOfBullet;
    private int currentAmountOfBullet;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float timeLife;
    [SerializeField] private float shootingCooldown;
    private float shootingTime;
    private NormalBullet script;
    private GameObject Go;
    private bool isShootingBulletSkill;

    [Header("Make swawp")]
    [SerializeField] private GameObject swawp;
    [SerializeField] private int amountOfSwawp;
    [SerializeField] private float swawpDamage;
    [SerializeField] private float swawpSpeedFly;
    [SerializeField] private float timeLifeSwawp;
    [SerializeField] private GameObject signObj;
    [SerializeField] private float timerSign;
    private SwawpBoss swawpBossScript;
    private GameObject swawpGo;
    private bool isSwawpSkill;

    private Transform cam;

    protected override void Start()
    {
        base.Start();
        cam = GameObject.Find("Main Camera").transform;
        SelectedSkill(1);
    }

    protected override void Update()
    {
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
        else if (isReadySkills[1])
        {
            isMove = false;
            isSwawpSkill = true;
            GetDestination();
            ResetCooldownSkill(selectedSkill);
        }

        if (isShootingBulletSkill)
        {
            Shooting();
        }else if (isSwawpSkill)
        {
            StartCoroutine(SpawnSwawpObj());
        }
    }

    //============================Shooting Skill======================================

    void Reload()
    {
        currentAmountOfBullet = amountOfBullet;
    }

    void Shooting()
    {
        if (currentAmountOfBullet > 0)
        {
            shootingTime -= Time.deltaTime;
            if (shootingTime <= 0)
            {
                shootingTime = shootingCooldown;
                SpawnBulletObj();
                currentAmountOfBullet--;
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
        float angle = Mathf.Atan2(GetDir().y, GetDir().x) * Mathf.Rad2Deg;
        Go = GameObject.Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, angle));
        script = Go.GetComponent<NormalBullet>();
        script.CreateBullet(bulletDamage, bulletSpeed, timeLife);
    }

    //=================================SwawpSkill=========================

    Vector3[] GetDestination()
    {
        Vector3[] destination = new Vector3[amountOfSwawp];
        int rd = Random.Range(0, amountOfSwawp);
        for (int i = 0; i < amountOfSwawp; i++)
        {

            if (i == rd)
            {
                destination[i] = target.position;
            }
            else
            {
                destination[i] = new Vector2(Random.Range(cam.position.x - 18, cam.position.y + 18),
                            Random.Range(cam.position.y - 7, cam.position.y + 7));
            }
        }
        return destination;
    }

    IEnumerator SpawnSwawpObj()
    {
        isSwawpSkill = false;
        Vector3[] destination = GetDestination();
        for (int i = 0; i < destination.Length; i++)
        {
            GameObject go = Instantiate(signObj, destination[i], Quaternion.identity);
            Destroy(go, timerSign + 2);
        }
        yield return new WaitForSeconds(timerSign);
        for (int i = 0; i < amountOfSwawp; i++)
        {
            swawpGo = Instantiate(swawp, transform.position, Quaternion.Euler(0, 0, Random.Range(0,360)));
            swawpBossScript = swawpGo.GetComponent<SwawpBoss>();
            swawpBossScript.CreateSwawp(destination[i], swawpDamage, swawpSpeedFly, timeLifeSwawp);
        }
        ChangeSkill(0);
        isMove = true;
    }
}
