using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private LayerMask whatIsShield;
    private float speed;
    private float timeLife;
    private Rigidbody2D rb;

    [SerializeField] private Vector2 sizecheck;
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
        if(isDamage)
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
        Collider2D hitShield = Physics2D.OverlapBox(transform.position, sizecheck, 0, whatIsShield);
        Collider2D enemy = Physics2D.OverlapBox(transform.position, sizecheck, 0, whatIsEnemy);
        if (hitShield)
        {
            isDamage = true;
            hitShield.transform.parent.SendMessage("DamageShield");
            return;
        }else if(enemy)
        {
            isDamage = true;
            enemy.transform.SendMessage("Damage", attackDetail);
        }
       
    }

    public void CreateBullet(float damage, float speed, float timeLife)
    {
        attackDetail.damage = damage;
        this.speed = speed;
        this.timeLife = timeLife;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, sizecheck);
    }
}
