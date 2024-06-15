using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSkill : MonoBehaviour
{
    [Header("Dice Pref")]
    [SerializeField] private GameObject diceObj;
    private GameObject go;
    private Dice script;


    [Header("Info")]
    [SerializeField] private float damageLevelUp;
    [Range(10, 100)]
    [SerializeField] private float damageLevelUpPercent;
    private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float timeLife;

    [Header("Cooldown skill")]
    [SerializeField] private float cooldown;
    private float timer;

    private int level;

    void Start()
    {
    }

    void Update()
    {
        if (level == 0) return;
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            DiceSpawner();
            timer = cooldown;
        }
    }

    public void LevelUp(int level)
    {
        this.level = level;
        damage = SkillManager.Instance.CalculateSkillDamage(damageLevelUp, damageLevelUpPercent, this.level);
    }

    void DiceSpawner()
    {
        float rotationZ;
        Vector3 direction;
        int amountOfEnemy = 0;
        GameObject enemyRam;
        Collider2D[] enemys = GetPositionInCam.Instance.GetEnemysPosition();
        for (int i = 0; i < this.level; i++)
        {
            if (enemys.Length > 0 && amountOfEnemy < enemys.Length)
            {
                amountOfEnemy++;
                enemyRam = enemys[i].gameObject;
                direction = (enemyRam.transform.position - transform.position).normalized;
                rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            }
            else
            {
                rotationZ = Random.Range(0, 360);
            }
            go = Instantiate(diceObj, transform.position, Quaternion.Euler(0, 0, rotationZ));
            script = go.GetComponent<Dice>();
            script.CreateDice(damage, speed, timeLife);
        }
    }
}
