using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_MoveState : EnemyMoveState
{
    private Boss1 boss;
    public Boss1_MoveState(Entity entity, StateMachine stateMachine, string isBoolName, EnemyMoveData data, Boss1 boss1) : base(entity, stateMachine, isBoolName, data)
    {
        this.boss = boss1;
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
        if (isPlayerDetected)
        {
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
