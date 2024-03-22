using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_DeathState : EnemyDeathState
{
    private Boss1 boss;
    public Boss1_DeathState(Entity entity, StateMachine stateMachine, string isBoolName, EnemyDeathData data, Boss1 boss) : base(entity, stateMachine, isBoolName, data)
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
    }

    public override void FinishAnimation()
    {
        base.FinishAnimation();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isFinishAnimation)
        {
            entity.DropItem();
            GameObject.Destroy(entity.gameObject);
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
