using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileBomb : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    private float speed;
    private float timeLife;
    private AttackDetail attackDetail;

    private Animator anim;
    private Rigidbody2D rb;
    private bool isDamage;
    private bool isWall;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attackDetail.attackDir = transform;
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDamage || isWall)
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("explode", true);
        }
        else
        {
            Destroy(gameObject, timeLife);
        }
        attackDetail.attackDir = transform;
    }

    private void FixedUpdate()
    {
        Attack();
    }

    void Attack()
    {
        Collider2D wall = Physics2D.OverlapCircle(attackPoint.position, radius, whatIsWall);
        Collider2D enemy = Physics2D.OverlapCircle(attackPoint.position, radius, whatIsEnemy);
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

    public void CreateBomb(float damage, float speed, float timeLife)
    {
        attackDetail.damage = damage;
        this.speed = speed;
        this.timeLife = timeLife;
    }

    void DestroyGO()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
