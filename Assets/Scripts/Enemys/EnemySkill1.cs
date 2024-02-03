using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill1 : MonoBehaviour
{
    [Header("Skill Point and Bullet")]
    [SerializeField] private Transform skillPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float timeLife;
    [Space]
    [Space]

    [Header("CooldownSkill")]
    [SerializeField] private float cooldown;
    private float timer;
    [Space]
    [Header("Amount bullet in 1 Skill")]
    [SerializeField] private int amountBullet;
    [Space]
    [Header("Amount Skill and cooldown")]
    [SerializeField] private int amountSkill;
    private int currentSkill;
    private float startTime;
    [SerializeField] private float skillTime;

    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        startTime = Time.time;
        currentSkill = amountSkill;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > cooldown)
        {
            if(currentSkill > 0)
            {
                enemy.isSkill = true;
                if (Time.time >= startTime + skillTime)
                {
                    currentSkill--;
                    startTime = Time.time;
                    Skill();
                }
            }
            else
            {
                currentSkill = amountSkill;
                enemy.isSkill = false;
                timer = 0;
            }

        }
    }

    void Skill()
    {
        int startPoint = Random.Range(0, 360);
        int t = (int)(360 / amountBullet);
        for (int i = 1; i <= amountBullet; i++)
        {
            SpawnBullet(skillPoint, Quaternion.Euler(0, 0,startPoint));
            startPoint += t;
        }
    }

    void SpawnBullet(Transform pos, Quaternion ro)
    {
        GameObject GO = Instantiate(bullet, pos.position, ro);
        Projectile script = GO.GetComponent<Projectile>();
        script.CreateBullet(damage, speed, timeLife);
    }
}
