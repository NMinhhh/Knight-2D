using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State
{
    protected bool isPlayerDetected;
    public EnemyIdleState(Entity entity, StateMachine stateMachine, string isBoolName) : base(entity, stateMachine, isBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
        isPlayerDetected = entity.CheckPlayer();
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocityZero();
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
        entity.SetVelocityZero();
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
