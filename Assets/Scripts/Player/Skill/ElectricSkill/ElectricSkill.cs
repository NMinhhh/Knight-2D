using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSkill : MonoBehaviour
{
    [Header("Electric Obj")]
    [SerializeField] private Electric[] electric;
    private Electric script;

    [Header("Cooldown")]
    [SerializeField] private float cooldown;
    private float timer;

    [Header("Info")]
    [SerializeField] private float damageLevelUp;
    [Range(10,100)]
    [SerializeField] private float damageLevelUpPercent;
    private float damage;
    [SerializeField] private float damageTime;

    [Header("Time electric attack")]
    [SerializeField] private float timeLife;
    private float timeLifeCur;
    private bool isSkillOn;
    private bool isSkillOff;
    
    [SerializeField] private List<GameObject> enemies;
    private int level;

    // Start is called before the first frame update
    void Start()
    {
        timeLifeCur = timeLife;
        timer = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (level == 0) return;

        timer -= Time.deltaTime;

        if (timer <= 0 && !isSkillOff && !isSkillOn)
        {
            isSkillOn = true;
        }

        if (isSkillOn)
        {
            StartSkill();
            timeLifeCur -= Time.deltaTime;
            if (timeLifeCur <= 0)
            {
                timeLifeCur = timeLife;
                isSkillOn = false;
                isSkillOff = true;
            }
        }
        else if(isSkillOff)
        {
            EndSkill();
            timer = cooldown;
            isSkillOff = false;
        }
    }

    public void LevelUp(int level)
    {
        this.level = level;
        damage = SkillManager.Instance.CalculateSkillDamage(damageLevelUp, damageLevelUpPercent, this.level);
        for(int i = 0; i < level; i++)
        {
            electric[i].SetElectric(damage, damageTime);
        }
    }

    void StartSkill()
    {
        Collider2D[] hits = GetPositionInCam.Instance.GetEnemysPosition();
        GameObject enemyRan;
        if (hits.Length > 0)
        {
            for (int i = 0; i < level; i++)
            {
                enemyRan = hits[Random.Range(0, hits.Length)].gameObject;
                if (!enemies.Contains(enemyRan) && enemies[i] == null)
                {
                    enemies[i] = enemyRan;
                }
            }
        }
        
        for (int i = 0; i < level; i++)
        {
            if (enemies[i] != null)
            {
                electric[i].ElectricAttack(transform.position, enemies[i].transform.position, enemies[i]);
            }
            else
            {
                electric[i].Draw2dRay(transform.position, transform.position);
            }
        }
    }

    void EndSkill()
    {
        for (int i = 0; i < level; i++)
        {
            electric[i].Draw2dRay(transform.position, transform.position);
        }
    }


    



   

   
}
