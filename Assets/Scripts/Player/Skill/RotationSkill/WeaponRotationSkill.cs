using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class WeaponRotationSkill : MonoBehaviour
{
    [SerializeField] private float valueRotationZ;

    [SerializeField] private float damage;
    [SerializeField] private GameObject[] skillObject;
    
    private float rotationZ;

    AttackDetail attackDetail;

    void Start()
    {
        attackDetail.attackDir = transform;
    }

    public void SetSkill(int level)
    {
        if(level > 1)
        {
            skillObject[level - 2].SetActive(false);
        }
        skillObject[level - 1].SetActive(true);
        CreateObj(skillObject[level - 1]);
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
