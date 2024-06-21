using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingSkill : MonoBehaviour
{
    [Header("Info")]
    [Range(0,100)]
    [SerializeField] private float[] slowingSpeedPercent;
    private float currentSlowingSpeedPercent;
    [SerializeField] private float damageLevelUp;
    [Range(0, 100)]
    [SerializeField] private float damageLevelUpPercent;
    private float damage;

    [Header("Cooldown")]
    [SerializeField] private float cooldown;
    private float timer;

    [Space]
    [Header("Radius check enemy")]
    [SerializeField] private float radius;


    [SerializeField] private LayerMask whatIsEnemy;

    private bool isSkill;

    private int level;

    private Animator anim;

    private AttackDetail attackDetail;

    void Start()
    {
        anim = GetComponent<Animator>();
        attackDetail.attackDir = transform;
        timer = cooldown;
    }

    void Update()
    {
        if (!isSkill) return;
        Slowing();

        timer += Time.deltaTime;
        if (timer >= cooldown)
        {
            Attack();
            timer = 0;
        }
    }

    public void LevelUp(int level)
    {
        this.level = level;
        if (!isSkill)
        {
            isSkill = true;
            anim.SetBool("isSkill", isSkill);
        }
        currentSlowingSpeedPercent = slowingSpeedPercent[this.level - 1];
        damage = SkillManager.Instance.CalculateSkillDamage(damageLevelUp, damageLevelUpPercent, this.level);

    }

    void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, whatIsEnemy);
        attackDetail.damage = damage;
        attackDetail.attackDir = transform;
        attackDetail.continousDamage = false;

        foreach (Collider2D hit in hits)
        {
            if (hit)
            {
                hit.transform.SendMessage("Damage", attackDetail);
            }
        }
    }

    public void Slowing()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, whatIsEnemy);
        foreach (Collider2D hit in hits)
        {
            if (hit)
            {
                hit.transform.SendMessage("IsSlowingEffect", currentSlowingSpeedPercent);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    // Update is called once per frame

}
