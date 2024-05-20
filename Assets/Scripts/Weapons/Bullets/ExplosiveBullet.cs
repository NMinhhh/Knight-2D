using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class ExplosiveBullet : Rocket
{
    [Header("Spawn bullet after explode")]
    [SerializeField] private GameObject bulletObj;
    private GameObject go;
    [SerializeField] private int amountOfBullet;
    private Meteor script;
    protected override void Attack()
    {
        base.Attack();
        SpawnBulllet();
    }

    private void SpawnBulllet()
    {
        float rotationz;
        Vector3 direction;
        int amountOfEnemy = 0;
        GameObject enemyRam;
        Collider2D[] enemys = EnemysPosition.Instance.GetEnemysPosition();
        for (int i = 0; i < amountOfBullet; i++)
        {
            if (enemys.Length > 0 && amountOfEnemy < enemys.Length)
            {
                amountOfEnemy++;
                enemyRam = enemys[i].gameObject;
                direction = (enemyRam.transform.position - transform.position).normalized;
                rotationz = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            }
            else
            {
                rotationz = Random.Range(0, 360);
            }
            go = Instantiate(bulletObj, transform.position, Quaternion.Euler(0, 0, rotationz));
            script = go.GetComponent<Meteor>();
            script.CreateMeteor(attackDetail.damage, speed, timeLife);
        }
    }

}
