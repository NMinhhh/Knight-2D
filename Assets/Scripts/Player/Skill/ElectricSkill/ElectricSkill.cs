using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSkill : MonoBehaviour
{
    [SerializeField] private GameObject[] electricsObj;
    private Electric script;

    //amout laser
    private int level;

    //cooldown
    [SerializeField] private float cooldown;
    private float time;

    //Info 
    [SerializeField] private float damage;
    [SerializeField] private float damageTime;

    //Turn Skill
    [SerializeField] private float timeLife;
    private float timeLifeCur;
    private bool isSkillOn;

    // Start is called before the first frame update
    void Start()
    {
        timeLifeCur = timeLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (level < 0) return;

        time -= Time.deltaTime;
        if(time <= 0 && !isSkillOn)
        {
            isSkillOn = true;
        }

        if(isSkillOn)
        {
            StartSkill();
            timeLifeCur -= Time.deltaTime;
            if (timeLifeCur <= 0)
            {
                time = cooldown;
                timeLifeCur = timeLife;
                isSkillOn = false;
            }
        }
        else
        {
            EndSkill();
        }
    }

    public void LevelUp(int i)
    {
        level = i;

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
