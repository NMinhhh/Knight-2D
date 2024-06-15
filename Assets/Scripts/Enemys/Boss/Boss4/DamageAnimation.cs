using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAnimation : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private GameObject light2D;

    [SerializeField] private Transform damagePoint;
    private float damage;
    private float timeLife;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask whatIsEnemy;
    private AttackDetail attackDetail;

    private void Start()
    {
        Destroy(gameObject, timeLife);
    }

    private void Update()
    {
        if (DayNightCircle.Instance.isNight)
        {
            light2D.SetActive(true);
        }
        else
        {
            light2D.SetActive(false);
        }
    }

    public void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(damagePoint.position, radius, whatIsEnemy);
        attackDetail.attackDir = transform;
        attackDetail.damage = damage;
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
