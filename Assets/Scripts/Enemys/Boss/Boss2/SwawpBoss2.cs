using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.U2D;

public class SwawpBoss2 : MonoBehaviour
{
    [SerializeField] private Vector3 destination;
    [SerializeField] private float speed;
    [SerializeField] private float radiusDamage;
    [SerializeField] private float damage;
    [SerializeField] private float timeLife;
    [SerializeField] private LayerMask whatIsPlayer;
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

    public void CreateSwawp(Vector3 destination, float damage, float timeLife)
    {
        this.destination = destination;
        this.damage = damage;
        this.timeLife = timeLife;
    }

    void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radiusDamage, whatIsPlayer);
        attackDetail.attackDir = transform;
        attackDetail.damage = damage;
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
