using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotationSkill : MonoBehaviour
{
    [SerializeField] private float timeRotation;
    [SerializeField] private Player player;
    //[SerializeField] private BoxCollider2D collider2d;
   // [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private float damage;
    [SerializeField] private GameObject[] skillObject;
    
    [SerializeField] private float rotationZ;
    [SerializeField] private int level;

    AttackDetail attackDetail;
    // Start is called before the first frame update
    void Start()
    {
        attackDetail.attackDir = transform;
        level = GameManager.Instance.level;
        SetSkill(level);
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.Instance.level > level && level < skillObject.Length)
        {
            level++;
            SetSkill(level);
        }
    }


    void SetSkill(int level)
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
        rotationZ += timeRotation;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        if(rotationZ <= -360)
        {
            rotationZ = 0;
        }
        
    }
}
