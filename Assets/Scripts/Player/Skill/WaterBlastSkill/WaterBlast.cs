using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBlast : MonoBehaviour
{
 
    private float damage;
    private float speed;
    private float time;
    private Vector2 getDir;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask whatISEnemy;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private BoxCollider2D collider2d;

    AttackDetail attackDetail;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(getDir.x, getDir.y, 0) * speed * Time.deltaTime;
        time -= Time.deltaTime;
        Attack();
        if(time <= 0 || CheckWall())
        {
            Destroy(gameObject);
        }
    }

    bool CheckWall()
    {
        return Physics2D.OverlapBox(collider2d.bounds.center, collider2d.bounds.size, 0, whatIsWall);
    }

    void Attack()
    {
        Collider2D[] hit = Physics2D.OverlapBoxAll(collider2d.bounds.center, collider2d.bounds.size, 0, whatISEnemy);
        attackDetail.attackDir = transform;
        attackDetail.damage = damage;
        attackDetail.continousDamage = false;
        foreach (Collider2D col in hit)
        {
            if(col)
            {
                col.transform.SendMessage("Damage", attackDetail);
            }
        }
    }


    public void SetSkill(float damage, float speed, float time, Vector3 dir)
    {
        this.damage = damage;
        this.speed = speed;
        this.time = time;
        getDir = (dir - transform.position).normalized;
    }

}
