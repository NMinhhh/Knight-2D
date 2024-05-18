using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSkill : MonoBehaviour
{
    [Header("Electric Obj")]
    [SerializeField] private GameObject[] electricsObj;
    private Electric script;

    [Header("Cooldown")]
    [SerializeField] private float cooldown;
    private float timer;

    [Header("Info")]
    [SerializeField] private float basicDamage;
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
    private bool isSkillStay;

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
        if (level < 0) return;

        timer -= Time.deltaTime;

        if (timer <= 0 && !isSkillOff && !isSkillOn && !isSkillStay)
        {
            isSkillOn = true;
        }

        if (isSkillOn)
        {
            StartSkill();
            isSkillOn = false;
            isSkillStay = true;
        }
        else if (isSkillStay)
        {
            timeLifeCur -= Time.deltaTime;
            if (timeLifeCur <= 0)
            {
                timeLifeCur = timeLife;
                isSkillStay = false;
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
        damage = GameManager.Instance.Calculate(basicDamage, damageLevelUp, damageLevelUpPercent, this.level);
    }

    void StartSkill()
    {
        for(int i = 0; i < level; i++)
        {
            electricsObj[i].SetActive(true);
            script = electricsObj[i].GetComponent<Electric>();
            script.SetElectric(damage, damageTime);
        }
    }

    void EndSkill()
    {
        for (int i = 0; i < level; i++)
        {
            electricsObj[i].SetActive(false);
        }
    }


    



   

   
}
