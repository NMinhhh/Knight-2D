using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingSkill : MonoBehaviour
{
    [SerializeField] private float maxCooldown;
    private float cooldown;
    private float timer;

    [SerializeField] private float damage;
    [SerializeField] private float radius;


    [SerializeField] private LayerMask whatIsEnemy;

    private bool isSkill;

    private Animator anim;

    AttackDetail attackDetail;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cooldown = maxCooldown;
        timer = cooldown;
    }

    public void LevelUp()
    {
        if (!isSkill)
        {
            isSkill = true;
            anim.SetBool("isSkill", isSkill);
        }
        else
        {
            UpdateCooldown();
        }

    }

    void UpdateCooldown()
    {
        cooldown -= maxCooldown * .2f;
    }

    public void Attack()
    {
        attackDetail.attackDir = transform;
        attackDetail.damage = damage;
        attackDetail.continousDamage = false;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, whatIsEnemy);
        foreach (Collider2D hit in hits)
        {
            if (hit)
            {
                hit.transform.SendMessage("Damage", attackDetail);
                hit.transform.SendMessage("IsSlowingEffect");
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    // Update is called once per frame
    void Update()
    {
        if (!isSkill) return;
        timer += Time.deltaTime;
        if(timer >= cooldown)
        {
            Attack();
            timer = 0;
        }
    }
}
