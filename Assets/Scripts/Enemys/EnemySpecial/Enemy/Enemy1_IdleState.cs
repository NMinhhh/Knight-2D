using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_IdleState : EnemyIdleState
{
    private Enemy1 enemy;
    public Enemy1_IdleState(Entity entity, StateMachine stateMachine, string isBoolName, Enemy1 enemy) : base(entity, stateMachine, isBoolName)
    {
        this.enemy = enemy;
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
            stateMachine.ChangeState(enemy.MoveState);
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
