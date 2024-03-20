using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : State
{
    protected EnemyMoveData data;
    protected bool isPlayerDetected;
    public EnemyMoveState(Entity entity, StateMachine stateMachine, string isBoolName, EnemyMoveData data) : base(entity, stateMachine, isBoolName)
    {
        this.data = data;
    }

    public override void DoCheck()
    {
        base.DoCheck();
        isPlayerDetected = entity.CheckPlayer();
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
        entity.SetMovement(data.movementSpeed);
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
