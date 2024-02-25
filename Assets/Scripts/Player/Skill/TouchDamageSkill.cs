using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TouchDamageSkill : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private float radius;
    [SerializeField] private float cooldown;
    private float damage;
    private float timer;
    [SerializeField] private LayerMask whatIsEnemy;

    AttackDetail attackDetail;
    // Start is called before the first frame update
    void Start()
    {
        timer = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        TakeDamage();
    }

    public void SetSkill(float damage)
    {
        this.damage = damage;
    }

    void TakeDamage()
    {
        timer += Time.deltaTime;
        attackDetail.attackDir = transform;
        attackDetail.damage = this.damage;
        Collider2D[] hit = Physics2D.OverlapCircleAll(point.position, radius, whatIsEnemy);
        foreach (Collider2D col in hit)
        {
            if (col && timer >= cooldown)
            {
                col.transform.SendMessage("Damage", attackDetail);
                timer = 0;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point.position, radius);
    }
}