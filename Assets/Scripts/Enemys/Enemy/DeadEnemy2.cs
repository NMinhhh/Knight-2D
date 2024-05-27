using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemy2 : Enemy
{
    [Header("Bullet Pref")]
    [SerializeField] private GameObject bulletObj;
    private NormalBullet script;
    private GameObject go;
    [Space]

    [Header("Amount Of Bullet")]
    [SerializeField] private int amountOfBullet;

    [Header("Bullet Info")]
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletTimeLife;
    protected override void Dead()
    {
        base.Dead();
        ShootingBullet();
    }
    void ShootingBullet()
    {
        float angle;
        for (int i = 0; i < amountOfBullet; i++)
        {
            if(i == 0)
            {
                angle = Mathf.Atan2(GetDir().y, GetDir().x) * Mathf.Rad2Deg;
            }
            else
            {
                angle = Random.Range(0, 360);
            }
            go = Instantiate(bulletObj, transform.position, Quaternion.Euler(0, 0, angle));
            script = go.GetComponent<NormalBullet>();
            script.CreateBullet(bulletDamage, bulletSpeed, bulletTimeLife);
        }
    }
}
