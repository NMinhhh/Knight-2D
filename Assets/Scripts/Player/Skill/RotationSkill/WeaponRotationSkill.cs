using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        skillObject[level - 1].SetActive(true);
        TouchDamageSkill script = skillObject[level - 1].GetComponent<TouchDamageSkill>();
        script.SetSkill(damage);
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
