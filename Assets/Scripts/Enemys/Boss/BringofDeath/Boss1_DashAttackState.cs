using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_DashAttackState : EnemyDashAttackState
{
    private Boss1 boss;
    public Boss1_DashAttackState(Entity entity, StateMachine stateMachine, string isBoolName, EnemyDashAttackData data, Transform attackPoint, Boss1 boss) : base(entity, stateMachine, isBoolName, data, attackPoint)
    {
        this.boss = boss;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        entity.SelectedStateAttack(0);
    }

    public override void FinishAnimation()
    {
        base.FinishAnimation();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(!entity.isDash && !isFinishAnimation)
        {
            entity.anim.SetBool("endDash", true);
            entity.SetVelocityZero();
        }
        else if (isFinishAnimation)
        {
            entity.anim.SetBool("endDash", false);
            stateMachine.ChangeState(boss.IdleState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public override void TriggerAnimation()
    {
        base.TriggerAnimation();
    }
}
