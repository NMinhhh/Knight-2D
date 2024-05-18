using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private int amoutOfSwawp;
    [SerializeField] private float swawpDamage;
    [SerializeField] private float timeLifeSwawp;
    private SwawpBoss2 swawpBossScript;
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
            ResetCooldownSkill(selectedSkill);
        }

        if (isShootingBulletSkill)
        {
            Shooting();
        }else if (isSwawpSkill)
        {
            SpawnSwawpObj();
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

    void SpawnSwawpObj()
    {
        Vector3 destination;
        float angle;
        int rd = Random.Range(0, amoutOfSwawp);
        for (int i = 0; i < amoutOfSwawp; i++)
        { 
            angle = Mathf.Atan2(GetDir().y, GetDir().x) * Mathf.Rad2Deg;
            swawpGo = GameObject.Instantiate(swawp, transform.position, Quaternion.Euler(0, 0, angle));
            swawpBossScript = swawpGo.GetComponent<SwawpBoss2>();
            if(i == rd)
            {
                destination = target.position;
            }
            else
            {
                destination = new Vector2(Random.Range(cam.position.x - 18, cam.position.y + 18), 
                            Random.Range(cam.position.y - 7, cam.position.y + 7));
            }
            swawpBossScript.CreateSwawp(destination, swawpDamage, timeLifeSwawp);
        }
        ChangeSkill(0);
        isMove = true;
        isSwawpSkill = false;
    }
}
