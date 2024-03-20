using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBomb : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private LayerMask whatIsShield;
    [SerializeField] private Transform checkPoint;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private float radiusDamage;
    private float speed;
    private float timeLife;
    private AttackDetail attackDetail;

    private Animator anim;
    private Rigidbody2D rb;
    private bool isDamage;
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
        if(isDamage)
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
        Check();
    }

    void Check()
    {
        Collider2D enemy = Physics2D.OverlapCircle(checkPoint.position, radius, whatIsEnemy);
        if (enemy && !isDamage)
        {
            isDamage = true;
        }
     
    }

    void Attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.position, radiusDamage, whatIsEnemy);
        Collider2D hitShield = Physics2D.OverlapCircle(checkPoint.position, radius, whatIsShield);
        attackDetail.continousDamage = false;
        if (hitShield)
        {
            hitShield.transform.parent.SendMessage("DamageShield");
            return;
        }
        foreach(Collider2D col in enemy)
        {
            if(col)
            {
                col.transform.SendMessage("Damage", attackDetail);
            }
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
        Gizmos.DrawWireSphere(checkPoint.position, radius);
        Gizmos.DrawWireSphere(attackPoint.position, radiusDamage);
    }
}
