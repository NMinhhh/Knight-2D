using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    private float damage;
    private float timeLife;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private LayerMask whatIsShield;

    AttackDetail attackDetail;
    void Start()
    {
        attackDetail.attackDir = transform;
        Destroy(gameObject, timeLife);
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Attack()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, radius, whatIsPlayer);
        Collider2D shield = Physics2D.OverlapCircle(transform.position, radius, whatIsShield);
        attackDetail.damage = damage;
        attackDetail.attackDir = transform;
        if (shield)
        {
            shield.transform.SendMessage("DamageShield");
            return;
        }
        if (player)
        {
            player.transform.SendMessage("Damage", attackDetail);
        }
    }

    public void CreateAcidSwawp(float damage, float timeLife)
    {
        this.damage = damage;
        this.timeLife = timeLife;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
