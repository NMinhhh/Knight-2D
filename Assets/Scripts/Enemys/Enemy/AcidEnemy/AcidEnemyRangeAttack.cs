using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEnemyRangeAttack : AcidEnemy
{
    [Header("Bullet Pref")]
    [SerializeField] private GameObject bulletPref;
    private NormalBullet scriptBullet;
    private GameObject bulletGo;
    [Space]

    [Header("Shooting Point and Check Player")]
    [SerializeField] private Transform shootigPoint;
    [SerializeField] private Transform checkPlayerDetected;
    [SerializeField] private Vector2 checkPlayerDetectedSize;
    [Space]

    [Header("Bullet Info")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletTimeLife;
    [Space]

    [Header("Shooting cooldown")]
    [SerializeField] private float shootCooldown;
    private float shootTimer;
    [Space]

    [Header("Idle timer")]
    [SerializeField] private float idleCooldown;
    private float idleTimer;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        ShootingBullet();
    }

    void ShootingBullet()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootCooldown && CheckPlayerDetected())
        {
            idleTimer = idleCooldown;
            shootTimer = 0;
            CreateBullet();
        }

        if (idleTimer > 0)
        {
            idleTimer -= Time.deltaTime;
            isMove = false;
        }
        else
        {
            isMove = true;
        }

    }

    private void CreateBullet()
    {
        float angle = Mathf.Atan2(GetDir().y, GetDir().x) * Mathf.Rad2Deg;
        bulletGo = Instantiate(bulletPref, transform.position, Quaternion.Euler(0, 0, angle));
        scriptBullet = bulletGo.GetComponent<NormalBullet>();
        scriptBullet.CreateBullet(bulletDamage, bulletSpeed, bulletTimeLife);
    }

    bool CheckPlayerDetected()
    {
        return Physics2D.OverlapBox(checkPlayerDetected.position, checkPlayerDetectedSize, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireCube(checkPlayerDetected.position, checkPlayerDetectedSize);
    }
}
