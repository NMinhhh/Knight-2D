using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy1_DeathState : EnemyDeathState
{
    private Enemy1 enemy;
    public Enemy1_DeathState(Entity entity, StateMachine stateMachine, string isBoolName, EnemyDeathData data) : base(entity, stateMachine, isBoolName, data)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        GameObject.Instantiate(data.particle, entity.transform.position, Quaternion.identity);
        for (int i = 0; i < data.amountOfEx; i++)
        {
            dropItemPoint = Random.insideUnitCircle * data.radius;
            entity.DropItem(entity.transform.position + dropItemPoint);
        }
        GameObject.Destroy(entity.gameObject);
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
