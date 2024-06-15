using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TouchDamageSkill : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private float radius;
    private float damage;
    [SerializeField] private LayerMask whatIsEnemy;

    AttackDetail attackDetail;

    // Update is called once per frame
    void Update()
    {
        attackDetail.attackDir = transform;
        TakeDamage();
    }

    public void SetSkill(float damage)
    {
        this.damage = damage;
    }

    void TakeDamage()
    {
        attackDetail.damage = this.damage;
        attackDetail.continousDamage = true;
        Collider2D[] hit = Physics2D.OverlapCircleAll(point.position, radius, whatIsEnemy);
        foreach (Collider2D col in hit)
        {
            if (col)
            {
                SoundFXManager.Instance.PlaySound(SoundFXManager.Sound.SawHit);
                col.transform.SendMessage("Damage", attackDetail);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point.position, radius);
    }
}