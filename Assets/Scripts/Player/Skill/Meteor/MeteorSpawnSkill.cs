using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawnSkill : MonoBehaviour
{
    [Header("Meteor Pref")]
    [SerializeField] private GameObject meteorGo;
    private Meteor script;
    private GameObject go;

    [Header("Cooldown")]
    [SerializeField] private float cooldown;
    private float timer;

    [Header("Info")]
    [SerializeField] private float damageLevelUp;
    [Range(10, 100)]
    [SerializeField] private float damageLevelUpPercent;
    private float damage;
    [SerializeField] private Vector2 speed;
    [SerializeField] private float timeLife;

    private int level;

    void Start()
    {
        timer = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0 && level > 0)
        {
            MeteorSpawner();
            timer = cooldown;
        }
    }

    public void LevelUp(int level)
    {
        this.level = level;
        damage = SkillManager.Instance.CalculateSkillDamage(damageLevelUp, damageLevelUpPercent, this.level);
    }

    void MeteorSpawner()
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
            go = Instantiate(meteorGo, transform.position, Quaternion.Euler(0, 0, rotationZ));
            script = go.GetComponent<Meteor>();
            script.CreateMeteor(damage, Random.Range(speed.x, speed.y), timeLife);
        }
    }
}
