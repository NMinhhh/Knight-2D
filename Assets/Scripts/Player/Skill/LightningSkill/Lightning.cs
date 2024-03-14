using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] private Transform damagePoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask whatIsEnemy;
    private float damage;
    AttackDetail attackDetail;

    public void Set(float damage)
    {
        this.damage = damage;
    }

    void TriggerAnimation()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(damagePoint.position, radius, whatIsEnemy);
        attackDetail.attackDir = transform;
        attackDetail.damage = damage;
        attackDetail.continousDamage = false;
        foreach (Collider2D col in hit)
        {
            if(col)
            {
                col.transform.SendMessage("Damage", attackDetail);
            }
        }
    }


    void FinishAnimation()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePoint.position, radius);
    }
}
