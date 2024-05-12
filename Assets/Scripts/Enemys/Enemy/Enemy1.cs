using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    [SerializeField] private GameObject bulletObj;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletTimeLife;
    private Projectile script;
    private GameObject go;
    protected override void Dead()
    {
        base.Dead();
        ShootingBullet();
    }
    void ShootingBullet()
    {
        float angle = Mathf.Atan2(GetDir().y, GetDir().x)*Mathf.Rad2Deg;
        go = Instantiate(bulletObj, transform.position, Quaternion.Euler(0,0,angle));
        script = go.GetComponent<Projectile>();
        script.CreateBullet(bulletDamage, bulletSpeed, bulletTimeLife);
    }
}
