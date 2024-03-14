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

    [SerializeField] private BoxCollider2D boxCollider;
    private AttackDetail attackDetail;

    private bool isWall;
    private bool isDamage;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackDetail.attackDir = transform;
        rb.velocity = transform.right * speed;
    }
    private void Update()
    {
        if(isDamage || isWall)
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, timeLife);
        }
        attackDetail.attackDir = transform;
        attackDetail.continousDamage = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Attack();
    }

    void Attack()
    {
        Collider2D wall = Physics2D.OverlapBox(transform.position, boxCollider.bounds.size, 0, whatIsWall);
        Collider2D enemy = Physics2D.OverlapBox(transform.position, boxCollider.bounds.size, 0, whatIsEnemy);
        if (enemy && !isDamage)
        {
            isDamage = true;
            enemy.transform.SendMessage("Damage", attackDetail);
        }
        if (wall)
        {
            isWall = true;
        }
    }

    public void CreateBullet(float damage, float speed, float timeLife)
    {
        attackDetail.damage = damage;
        this.speed = speed;
        this.timeLife = timeLife;
    }
}
