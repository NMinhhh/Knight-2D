using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlowingState : State
{
    protected EnemySlowingData data;
    protected bool isSlowing;
    public EnemySlowingState(Entity entity, StateMachine stateMachine, string isBoolName, EnemySlowingData data) : base(entity, stateMachine, isBoolName)
    {
        this.data = data;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        isSlowing = true;
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
        if(Time.time >= startTime + data.slowingTimer)
        {
            isSlowing = false;
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
