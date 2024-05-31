using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawnSkill : MonoBehaviour
{
    [Header("Rocket Pref")]
    [SerializeField] private GameObject spawnGo;
    private GameObject go;
    private Rocket script;

    [Header("Cooldown")]
    [SerializeField] private float cooldown;
    private float timer;

    [Header("Info")]
    [SerializeField] private float damageLevelUp;
    [Range(10, 100)]
    [SerializeField] private float damageLevelUpPercent;
    private float damage;
    [SerializeField] private float speed;
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
        if (timer <= 0 && level > 0)
        {
            timer = cooldown;
            SetSkill(level);
        }
    }

    public void LevelUp(int level)
    {
        this.level = level;
        damage = SkillManager.Instance.CalculateSkillDamage(damageLevelUp, damageLevelUpPercent, this.level);
    }

    void SetSkill(int level)
    {
        float rotationZ;
        Vector3 direction;
        GameObject enemyRam;
        int amountOfEnemy = 0;
        Collider2D[] enemys = GetPositionInCam.Instance.GetEnemysPosition();
        for (int i = 0; i < level; i++) 
        {
            if(enemys.Length > 0 && amountOfEnemy < enemys.Length)
            {
                enemyRam = enemys[i].gameObject;
                amountOfEnemy++;
                direction = (enemyRam.transform.position - transform.position).normalized;
                rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            }
            else
            {
                rotationZ = Random.Range(0, 360);

            }
            go = Instantiate(spawnGo,transform.position,Quaternion.Euler(0,0,rotationZ));
            script = go.GetComponent<Rocket>();
            script.CreateBomb(damage, speed, timeLife);
        }
    }
}
