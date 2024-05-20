using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : Enemy
{
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform shootigPoint;
    [SerializeField] private Transform checkPlayerDetected;
    [SerializeField] private Vector2 checkPlayerDetectedSize;

    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletTimeLife;
    [SerializeField] private float shootCooldown;

    [SerializeField] private float idleCooldown;
    private float idleTimer;

    private float shootTimer;
    private NormalBullet script;
    private GameObject go;

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
        return Physics2D.OverlapBox(checkPlayerDetected.position, checkPlayerDetectedSize, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireCube(checkPlayerDetected.position, checkPlayerDetectedSize);
    }
}
