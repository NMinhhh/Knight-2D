using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnSkill : MonoBehaviour
{
    [Header("Component Bullets")]
    [SerializeField] private GameObject bullet;
    private GameObject go;
    private BulletMoveArc script;

    [Header("Info Bullet")]
    [SerializeField] private float damageLevelUp;
    [Range(10,100)]
    [SerializeField] private float damageLevelUpPercent;
    private float damage;
    [SerializeField] private float speed;
    [Range (10,100)]
    [SerializeField] private float speedLevelUpPercent;
    private float currentSpeed;

    [Header("Cooldown skill")]
    [SerializeField] private float cooldown;
    private float timer;

    [Header("Amount of bullet")]
    [SerializeField] private int amountBullet;
    private int amountOfBulletSpawn;
    private int amountOfBulletSpawnCur;

    [Header("Random point radius")]
    [SerializeField] private float radius;

    float cooldownShoting;


    private int level;

    void Start()
    {
        timer = cooldown;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && amountOfBulletSpawn > 0)
        {
            cooldownShoting += Time.deltaTime;
            if(cooldownShoting >= 0.1f && amountOfBulletSpawnCur > 0)
            {
                SpawnBullet();
                cooldownShoting = 0;
                amountOfBulletSpawnCur -= 1;
            }else if(amountOfBulletSpawnCur == 0)
            {
                timer = cooldown;
                amountOfBulletSpawnCur = amountOfBulletSpawn;
            }
        }
    }


    public void LevelUp(int level)
    {
        this.level = level;
        damage = SkillManager.Instance.CalculateSkillDamage(damageLevelUp, damageLevelUpPercent, this.level);
        amountOfBulletSpawn += amountBullet;
        amountOfBulletSpawnCur = amountOfBulletSpawn;
    }

    void SpawnBullet()
    {
        Vector3 point1, point2;
        GameObject enemyRam = null;
        Collider2D[] enemy = GetPositionInCam.Instance.GetEnemysPosition();
        if(enemy.Length > 0)
        {
            enemyRam = enemy[Random.Range(0,enemy.Length)].gameObject;
            point2 = Vector3.one;
        }
        else
        {
            point2 = GetPositionInCam.Instance.GetPositionInArea();
        }

        Vector3 p = Random.insideUnitCircle * radius;
        point1 = transform.position + p;

        go = Instantiate(bullet, transform.position, Quaternion.identity);
        script = go.GetComponent<BulletMoveArc>();

        script.CreateBlletObj(enemyRam, transform.position, point1, point2, damage, speed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
