using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private Transform checkPoint;
    [SerializeField] private float radius;
    [SerializeField] private GameObject particle;
    private float damage;
    private float speed;
    private float timeLife;

    [SerializeField] private LayerMask whatIsEnemy;

    private bool isDamage;

    private Rigidbody2D rb;

    AttackDetail attackDetail;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        attackDetail.attackDir = transform;
        timeLife -= Time.deltaTime;
        if(timeLife <= 0)
        {
            Destroy(gameObject);
        }
        Attack();
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