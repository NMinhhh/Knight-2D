using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss1 : MonoBehaviour
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
    private int amountBulletCurrent;
    [Space]
    [Header("Cooldown")]
    private float startTime;
    [SerializeField] private float skillTime;

    private bool isSkillStart;

    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        startTime = Time.time;
        amountBulletCurrent = amountBullet;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > cooldown)
        {
            enemy.isSkill = true;
            enemy.anim.SetBool("skill1", true);
            if (amountBulletCurrent > 0 && isSkillStart)
            {
                if (Time.time >= startTime + skillTime)
                {
                    amountBulletCurrent--;
                    startTime = Time.time;
                    SpawnBullet();
                }
            }
            else if(amountBulletCurrent <= 0)
            {
                amountBulletCurrent = amountBullet;
                enemy.anim.SetBool("endSkill", true);
                isSkillStart = false;
            }
        }
    }
    public void StartSkill()
    {
        enemy.anim.SetBool("startSkill", true);
        isSkillStart = true;
    }

    public void FinishSkill()
    {
        enemy.isSkill = false;
        timer = 0;
        enemy.anim.SetBool("skill1", false);
        enemy.anim.SetBool("startSkill", false);
        enemy.anim.SetBool("endSkill", false);
    }

    void SpawnBullet()
    {
        float angle = Mathf.Atan2(GetDir().y, GetDir().x) * Mathf.Rad2Deg;
        GameObject GO = Instantiate(bullet, skillPoint.position, Quaternion.Euler(0, 0, angle));
        Vector3 localScale = Vector3.one;
        if(angle > 90 || angle < -90)
        {
            localScale.y = -1;
        }
        else
        {
            localScale.y = 1;
        }
        GO.transform.localScale = localScale;
        ProjectileBomb script = GO.GetComponent<ProjectileBomb>();
        script.CreateBomb(damage, speed, timeLife);
    }

    Vector2 GetDir()
    {
        return (enemy.target.position - skillPoint.position).normalized;
    }
}
