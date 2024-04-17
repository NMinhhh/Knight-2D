using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_IdleState : EnemyIdleState
{
    private Boss1 boss;
    public Boss1_IdleState(Entity entity, StateMachine stateMachine, string isBoolName, Boss1 boss) : base(entity, stateMachine, isBoolName)
    {
        this.boss = boss;
    }

    public override void DoCheck()
    {
        base.DoCheck();
        entity.CheckIfFlip();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAnimation()
    {
        base.FinishAnimation();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isPlayerDetected)
        {
            stateMachine.ChangeState(boss.MoveState);
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
