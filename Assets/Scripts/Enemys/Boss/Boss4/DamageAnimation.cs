using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAnimation : MonoBehaviour
{
    [SerializeField] private Transform damagePoint;
    private float damage;
    private float timeLife;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private LayerMask whatIsShield;
    private AttackDetail attackDetail;

    private void Start()
    {
        Destroy(gameObject, timeLife);
    }

    public void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(damagePoint.position, radius, whatIsEnemy);
        Collider2D hitShield = Physics2D.OverlapCircle(damagePoint.position, radius, whatIsShield);
        attackDetail.attackDir = transform;
        attackDetail.damage = damage;
        if (hitShield)
        {
            hitShield.transform.SendMessage("DamageShield");
            return;
        }
        if (hit)
        {
            hit.transform.SendMessage("Damage", attackDetail);
        }
    }
    public void CreateObj(float damage, float timeLife)
    {
        this.damage = damage;
        this.timeLife = timeLife;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePoint.position, radius);
    }
}
