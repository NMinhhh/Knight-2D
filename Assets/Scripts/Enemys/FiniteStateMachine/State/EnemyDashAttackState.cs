using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDashAttackState : State
{
    protected EnemyDashAttackData data;
    protected Transform attackPoint;
    protected Vector2 dir;
    public EnemyDashAttackState(Entity entity, StateMachine stateMachine, string isBoolName, EnemyDashAttackData data, Transform attackPoint) : base(entity, stateMachine, isBoolName)
    {
        this.data = data;
        this.attackPoint = attackPoint;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        entity.GetCurrentTargetToDashing();
        entity.SetVelocityZero();
        dir = GetDir();
    }

    public override void Exit()
    {
        base.Exit();
        entity.IsDashing();
        entity.SetCooldownAttack(entity.stateAttackSelected);
        entity.SelectedStateAttack(entity.GetRandomAttackState());
    }

    public override void FinishAnimation()
    {
        base.FinishAnimation();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        entity.Dash(data.dashSpeed, dir);
        entity.CheckDistanceToStopDash(attackPoint.position, entity.currentTarget, data.distance);

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public override void TriggerAnimation()
    {
        base.TriggerAnimation();
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, data.radius, data.whatIsPlayer);
        Collider2D hitShield = Physics2D.OverlapCircle(attackPoint.position, data.radius, data.whatIsShield);
        if (hitShield)
        {
            hitShield.transform.parent.SendMessage("DamageShield");
            return;
        }
        entity.attackDetail.damage = data.damage;
        entity.attackDetail.attackDir = entity.transform;
        foreach (Collider2D col in hit)
        {
            if (col)
            {
                col.transform.SendMessage("Damage", entity.attackDetail);
            }
        }
    }

    public Vector2 GetDir()
    {
        return (entity.target.position - attackPoint.position).normalized;
    }
   
}
