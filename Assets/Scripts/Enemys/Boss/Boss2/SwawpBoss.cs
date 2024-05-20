using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.U2D;

public class SwawpBoss : MonoBehaviour
{
    private Vector3 destination;
    private float speed;
    private float damage;
    private float timeLife;
    [SerializeField] private float radiusDamage;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private LayerMask whatIsShield;
    private Animator anim;
    AttackDetail attackDetail;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if (Check())
        {
            anim.SetBool("isSwawp", true);
            Attack();
            Destroy(gameObject, timeLife);
        }
    }

    public void CreateSwawp(Vector3 destination, float damage, float speed, float timeLife)
    {
        this.destination = destination;
        this.damage = damage;
        this.speed = speed;
        this.timeLife = timeLife;
    }

    void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radiusDamage, whatIsPlayer);
        Collider2D hitShield = Physics2D.OverlapCircle(transform.position, radiusDamage, whatIsShield);
        attackDetail.attackDir = transform;
        attackDetail.damage = damage;
        if (hitShield)
        {
            hitShield.transform.parent.SendMessage("DamageShield");
            return;
        }
        if (hit)
        {
            hit.transform.SendMessage("Damage", attackDetail);
        }
    }

    bool Check()
    {
        return Vector3.Distance(transform.position, destination) <= .1f;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusDamage);
    }
}
