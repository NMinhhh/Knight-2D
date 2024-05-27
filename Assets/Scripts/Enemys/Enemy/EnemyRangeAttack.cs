using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : Enemy
{
    [Header("Bullet Pref")]
    [SerializeField] private GameObject bulletPref;
    private NormalBullet script;
    private GameObject go;
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
        shootTimer = shootCooldown;
    }

    protected override void Update()
    {
        base.Update();
        ShootingBullet();
    }

    void ShootingBullet()
    {
        shootTimer += Time.deltaTime;
        if(shootTimer >= shootCooldown && CheckPlayerDetected())
        {
            idleTimer = idleCooldown;
            shootTimer = 0;
            float angle = Mathf.Atan2(GetDir().y, GetDir().x) * Mathf.Rad2Deg;
            go = Instantiate(bulletPref, transform.position, Quaternion.Euler(0, 0, angle));
            script = go.GetComponent<NormalBullet>();
            script.CreateBullet(bulletDamage, bulletSpeed, bulletTimeLife);
        }
        
        if(idleTimer > 0)
        {
            idleTimer -= Time.deltaTime;
            isMove = false;
        }
        else
        {
            isMove = true;
        }

    }

    bool CheckPlayerDetected()
    {
        return Physics2D.OverlapBox(checkPlayerDetected.position, checkPlayerDetectedSize,0 , whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireCube(checkPlayerDetected.position, checkPlayerDetectedSize);
    }
}
