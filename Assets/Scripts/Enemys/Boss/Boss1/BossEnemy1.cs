using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy1 : BossEnemy
{
    [Header("Dash Skill")]
    [SerializeField] private float dashSpeed;
    private Vector3 currentTarget;
    protected bool isDash;
    [SerializeField] protected float distanceToStop;
    [Space]
    [Space]

    [Header("Shooting Bullet Skill")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private int amountOfBullet;
    private int currentAmountOfBullet;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float timeLife;
    [SerializeField] private float shootingCooldown;
    private float shootingTime;
    private Projectile script;
    private GameObject Go;
    private bool isShootingBulletSkill;

    protected override void Start()
    {
        base.Start();
        selectedSkill = 1;
    }

    protected override void Update()
    {
        base.Update();
        if (isCheckSkill)
            CheckCooldownSkill(selectedSkill);

        if (isReadySkills[0])
        {
            isMove = false;
            SetCurrentTarget();
            isDash = true;
            ResetCooldownSkill(selectedSkill);
        }
        else if (isReadySkills[1])
        {
            isMove = false;
            Reload();
            isShootingBulletSkill = true;
            ResetCooldownSkill(selectedSkill);
        }


        if (isDash)
        {
            Dash(dashSpeed);
        }
        else if (isShootingBulletSkill)
        {
            Shooting();
        }
    }
    //======================Dash Skill=============================
    void SetCurrentTarget()
    {
        currentTarget = target.position;
    }

    void DashEffect()
    {
        GameObject obj = new GameObject();
        SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
        sr.sprite = spriteRenderer.sprite;
        sr.color = new Color(1, 1, 1, 1);
        sr.flipX = true;
        sr.sortingLayerName = "Enemy";
        obj.transform.position = transform.position;
        obj.transform.localScale = transform.localScale;
        Destroy(obj, .1f);
    }

    public void CheckDistanceToStopDash(Vector3 a, Vector3 b, float distance)
    {
        if (Vector2.Distance(a, b) <= distance)
        {
            isDash = false;
            SetVelocityZero();
            isMove = true;
            ChangeSkillRandom();
        }
    }

    public void Dash(float dashSpeed)
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, dashSpeed * Time.deltaTime);
        DashEffect();
        CheckDistanceToStopDash(transform.position, currentTarget, distanceToStop);
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
        script = Go.GetComponent<Projectile>();
        script.CreateBullet(bulletDamage, bulletSpeed, timeLife);
    }
}
