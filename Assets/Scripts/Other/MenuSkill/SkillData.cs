using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="newSkillData",menuName ="Skill/Skill Data")]
public class SkillData : ScriptableObject
{
    public Skill[] skills;

    public int GetSkillLength()
    {
        return this.skills.Length;
    }

    public Skill GetSkill(int index)
    {
        return this.skills[index];
    }

    public void LevelUp(int id)
    {
        this.skills[id].level++;
    }

    public void ResetLevelSkill()
    {
        foreach (Skill skill in skills)
        {
            skill.level = 1;
        }
    }
}
