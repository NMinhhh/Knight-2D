using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private LayerMask whatIsWall;
    private float speed;
    private float timeLife;
    private Rigidbody2D rb;

    [SerializeField]private BoxCollider2D boxCollider;
    private AttackDetail attackDetail;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        DestroyProj();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Attack();
        attackDetail.attackDir = transform;
        if (CheckWall())
        {
            Destroy(gameObject);
        }
    }

    bool CheckWall()
    {
        return Physics2D.OverlapBox(transform.position, boxCollider.bounds.size, 0, whatIsWall);
    }

    void Attack()
    {
        Collider2D[] hit = Physics2D.OverlapBoxAll(transform.position, boxCollider.bounds.size, 0, whatIsEnemy);
        foreach (Collider2D hit2 in hit)
        {
            if (hit2)
            {
                hit2.transform.SendMessage("Damage", attackDetail);
                Destroy(gameObject);
            }
        }
    }
    void DestroyProj()
    {
        Destroy(gameObject,timeLife);
    }

    public void CreateBullet(float damage, float speed, float timeLife)
    {
        attackDetail.damage = damage;
        this.speed = speed;
        this.timeLife = timeLife;
    }
}
