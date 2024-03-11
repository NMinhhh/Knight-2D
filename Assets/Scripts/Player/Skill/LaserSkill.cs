using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSkill : MonoBehaviour
{
    [SerializeField] private GameObject[] lasers;
    private Laser script;


    //amout laser
    private int amoutLaser;

    //cooldown
    [SerializeField] private float cooldown;
    private float time;

    //Info 
    [SerializeField] private float damage;

    //Turn Skill
    [SerializeField] private float timeLife;
    private float timeLifeCur;
    private bool isSkillOn;

    // Start is called before the first frame update
    void Start()
    {
        time = cooldown;
        timeLifeCur = timeLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (amoutLaser < 0) return;

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

    public void AddLaser(int i)
    {
        amoutLaser = i;

    }

    void StartSkill()
    {
        for(int i = 0; i < amoutLaser; i++)
        {
            lasers[i].SetActive(true);
            script = lasers[i].GetComponent<Laser>();
            script.SetLaser(damage);
        }
    }

    void EndSkill()
    {
        for (int i = 0; i < amoutLaser; i++)
        {
            lasers[i].SetActive(false);
        }
    }


    



   

   
}
