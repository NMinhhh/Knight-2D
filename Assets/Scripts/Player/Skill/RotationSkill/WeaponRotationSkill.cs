using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class WeaponRotationSkill : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float valueRotationZ;

    [Header("Damage")]
    [SerializeField] private float damageLevelUp;
    [Range(10, 100)]
    [SerializeField] private float damageLevelUpPercent;
    private float damage;

    [Header("Skill Obj")]
    [SerializeField] private GameObject[] skillObject;
    
    private float rotationZ;

    AttackDetail attackDetail;

    private int level;

    void Start()
    {
        attackDetail.attackDir = transform;
    }

    public void LevelUp(int level)
    {
        this.level = level;
        int objPos = level - 1;
        if(level > 1)
        {
            skillObject[objPos - 1].SetActive(false);
        }
        damage = SkillManager.Instance.CalculateSkillDamage(damageLevelUp, damageLevelUpPercent, this.level);
        skillObject[objPos].SetActive(true);
        CreateObj(skillObject[objPos]);
    }

    void CreateObj(GameObject content)
    {
        for(int  i= 0; i < content.transform.childCount; i++)
        {
            TouchDamageSkill script = content.transform.GetChild(i).gameObject.GetComponent<TouchDamageSkill>();
            script.SetSkill(damage);
        }
    }

    void FixedUpdate()
    {
        RotationZ();
    }

    public void RotationZ()
    {
        rotationZ += valueRotationZ;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        if(rotationZ <= -360)
        {
            rotationZ = 0;
        }
        
    }
}
