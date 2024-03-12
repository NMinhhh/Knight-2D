using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private float radius;
    private float damage;
    AttackDetail attackDetail;

    public void Set(float damage)
    {
        this.damage = damage;
    }

    void TriggerAnimation()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, radius, whatIsEnemy);
        attackDetail.attackDir = transform;
        attackDetail.damage = damage;
        foreach (Collider2D col in hit)
        {
            if(col != null)
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
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
