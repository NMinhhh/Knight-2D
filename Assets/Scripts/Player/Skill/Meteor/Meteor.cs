using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [Header("Point Damage")]
    [SerializeField] private Transform checkPoint;
    [SerializeField] private float radius;
    [SerializeField] private GameObject particle;
    private float damage;
    private float speed;
    private float timeLife;
    [Space]
    [Space]

    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private LayerMask whatIsWall;

    private Rigidbody2D rb;

    AttackDetail attackDetail;
    void Start()
    {
        attackDetail.attackDir = transform;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();

        timeLife -= Time.deltaTime;
        if(timeLife <= 0 || CheckWall())
        {
            Destroy(gameObject);
        }
    }

    bool CheckWall()
    {
        return Physics2D.OverlapCircle(checkPoint.position, radius, whatIsWall);
    }

    public void CreateMeteor(float damage, float speed, float timeLife)
    {
        this.damage = damage;
        this.speed = speed;
        this.timeLife = timeLife;
    }


    void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(checkPoint.position, radius, whatIsEnemy);
        attackDetail.damage = damage;
        attackDetail.attackDir = transform;
        attackDetail.continousDamage = true;
        if (hit)
        {
            Instantiate(particle, hit.transform.position, Quaternion.identity);
            hit.transform.SendMessage("Damage", attackDetail);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkPoint.position, radius);
    }
}
