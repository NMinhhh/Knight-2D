using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : Enemy
{
    [Header("Check player to dash")]
    [SerializeField] private Transform checkPlayerDetected;
    [SerializeField] private Vector2 checkPlayerDetectedSize;
    [Space]

    [Header("Attack Point")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Vector2 attackSize;
    [SerializeField] private float attackDamage;

    [SerializeField] private float attackCooldown;
    private float attackTimer;

    protected bool isAttack;

    private AttackDetail meleeAttackDetail;

    protected override void Start()
    {
        base.Start();
        attackTimer = attackCooldown;
    }

    protected override void Update()
    {
        base.Update();
        attackTimer += Time.deltaTime;
        if(attackTimer > attackCooldown && CheckPlayerDetected() && !isAttack)
        {
            isLock = true;
            isAttack = true;
            isMove = false;
            anim.SetBool("meleeAttack", true);
        }

    }

    public void MeleeAttack()
    {
        Collider2D player = Physics2D.OverlapBox(attackPoint.position, attackSize, transform.eulerAngles.z, whatIsPlayer);
        meleeAttackDetail.damage = attackDamage;
        meleeAttackDetail.attackDir = transform;
        if (player)
        {
            player.transform.SendMessage("Damage", meleeAttackDetail);
        }
    }

    public void AttackFinish()
    {
        anim.SetBool("meleeAttack", false);
        isLock = false;
        isMove = true;
        attackTimer = 0;
        isAttack = false;
    }

    bool CheckPlayerDetected()
    {
        return Physics2D.OverlapBox(checkPlayerDetected.position, checkPlayerDetectedSize, 0, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireCube(checkPlayerDetected.position, checkPlayerDetectedSize);
        Gizmos.DrawWireCube(attackPoint.position, attackSize);
    }
}
