using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnSkill : MonoBehaviour
{
    [Header("Component Bullets")]
    [SerializeField] private Transform[] spawnPos;
    [SerializeField] private GameObject bullet;
    private GameObject go;
    private Projectile script;

    [Header("Info Bullet")]
    [SerializeField] private float damge;
    [SerializeField] private Vector2 speed;
    [SerializeField] private float timeLife;

    [Header("Cooldown skill")]
    [SerializeField] private float cooldown;
    private float timer;
    [SerializeField] private int amountBullet;
    private int amountOfBulletCur;


    float cooldownShoting;
    //AmountOf point spawn Bullet


    void Start()
    {
        timer = cooldown;
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && amountBullet > 0)
        {
            cooldownShoting += Time.deltaTime;
            if(cooldownShoting >= 0.1f && amountOfBulletCur > 0)
            {
                SetSkill(spawnPos[0]);
                cooldownShoting = 0;
                amountOfBulletCur -= 1;
            }else if(amountOfBulletCur == 0)
            {
                timer = cooldown;
                amountOfBulletCur = amountBullet;
            }
        }
    }


    public void AddAmountOfBullet()
    {
        if (!spawnPos[0].gameObject.activeInHierarchy)
            spawnPos[0].gameObject.SetActive(true);
        amountBullet += 3;
        amountOfBulletCur = amountBullet;
    }

    void SetSkill(Transform pos)
    {
        float rotationZ;
        Vector3 direction;
        GameObject enemyRam;
        Collider2D[] enemy = EnemysPosition.Instance.GetEnemysPosition();
        if(enemy.Length > 0)
        {
            enemyRam = enemy[Random.Range(0,enemy.Length)].gameObject;
            direction = (enemyRam.transform.position - pos.position).normalized;
            rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
        else
        {
            rotationZ = Random.Range(0, 360);
        }
        go = Instantiate(bullet, pos.position, Quaternion.Euler(0, 0, rotationZ));
        script = go.GetComponent<Projectile>();
        script.CreateBullet(damge, Random.Range(speed.x, speed.y), timeLife); 
    }

    

}
