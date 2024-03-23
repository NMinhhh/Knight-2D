using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_MoveState : EnemyMoveState
{
    private Enemy1 enemy;
    public Enemy1_MoveState(Entity entity, StateMachine stateMachine, string isBoolName, EnemyMoveData data, Enemy1 enemy) : base(entity, stateMachine, isBoolName, data)
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
        if (isPlayerDetected)
        {
            stateMachine.ChangeState(enemy.IdleState);
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
