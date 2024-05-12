using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BossEnemy : Enemy
{
    [Header("Cooldown Skill")]
    [SerializeField] private List<float> cooldownSkills;
    protected List<float> currentCooldownSkills;
    protected bool[] isReadySkills;
    protected int selectedSkill;
    protected bool isCheckSkill;

    protected override void Start()
    {
        base.Start();
        isCheckSkill = true;
        currentCooldownSkills = new List<float>();
        currentCooldownSkills.AddRange(cooldownSkills);
        isReadySkills = new bool[currentCooldownSkills.Count];
       
    }

    protected override void Update()
    {
        base.Update();
        
    }
    protected virtual void ChangeSkillRandom()
    {
        SelectedSkill(GetRandomSkill());
        isCheckSkill = true;
    }

    protected virtual void ChangeSkill(int id)
    {
        SelectedSkill(id);
        isCheckSkill = true;
    }

    protected virtual int GetRandomSkill()
    {
        return Random.Range(0, currentCooldownSkills.Count);
    }
    protected virtual void SelectedSkill(int idx)
    {
        selectedSkill = idx;
    }

    protected virtual void ResetCooldownSkill(int idx)
    {
        isCheckSkill = false;
        currentCooldownSkills[idx] = cooldownSkills[idx];
        isReadySkills[idx] = false;
    }

    protected virtual void CheckCooldownSkill(int id)
    {
        currentCooldownSkills[id] -= Time.deltaTime;
        if (currentCooldownSkills[id] <= 0)
        {
            isReadySkills[id] = true;
        }
    }
}
