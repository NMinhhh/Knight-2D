using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSkill : MonoBehaviour
{
    [Header("Laser Obj")]
    [SerializeField] private GameObject[] laser;
    Laser script;

    [Header("Damage")]
    [SerializeField] private float damageLevelUp;
    [Range(10, 100)]
    [SerializeField] private float damageLevelUpPercent;
    private float damage;

    [Header("Cooldown")]
    [SerializeField] private float cooldown;
    private float timer;

    private int level;

    // Update is called once per frame
    void Update()
    {
       timer += Time.deltaTime;
        if(timer > cooldown && level > 0)
        {
            SetSkill();
            timer = 0;
        }
    }

    public void LevelUp(int level)
    {
        this.level = level;
        damage = SkillManager.Instance.CalculateSkillDamage(damageLevelUp, damageLevelUpPercent, this.level);
        UpdateLaser();
    }

    void UpdateLaser()
    {
        for (int i = 0; i < level; i++)
        {
            script = laser[i].GetComponent<Laser>();
            script.CreateLaser(damage);
        }
    }

    void SetSkill()
    {
        float rotationz;
        Vector3 direction;
        int amountOfEnemy = 0;
        GameObject enemyRam;
        Collider2D[] enemys = GetPositionInCam.Instance.GetEnemysPosition();
        for (int i = 0; i < level; i++)
        {
            if (enemys.Length > 0 && amountOfEnemy < enemys.Length)
            {
                amountOfEnemy++;
                enemyRam = enemys[i].gameObject;
                direction = (enemyRam.transform.position - laser[i].transform.position).normalized;
                rotationz = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            }
            else
            {
                enemyRam = null;
                rotationz = Random.Range(0, 360);
            }

            script = laser[i].GetComponent<Laser>();
            laser[i].transform.eulerAngles = new Vector3(0, 0, rotationz);

            if(enemyRam == null)
            {
                script.OnLaser();
            }
            else
            {
               script.OnLaser(enemyRam);
            }
        }
    }
}
