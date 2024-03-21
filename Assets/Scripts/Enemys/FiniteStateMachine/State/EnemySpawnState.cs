using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnState : State
{
    protected Transform spawnPoint;
    protected EnemySpawnData data;
    protected bool isStartSpawn;
    protected int amountOfSpawnGO;
    public EnemySpawnState(Entity entity, StateMachine stateMachine, string isBoolName, Transform spawnPoint, EnemySpawnData data) : base(entity, stateMachine, isBoolName)
    {
        this.spawnPoint = spawnPoint;
        this.data = data;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        amountOfSpawnGO = data.amountOfSpawnGO;
        entity.SetVelocityZero();
        isStartSpawn = false;
    }

    public override void Exit()
    {
        base.Exit();
        entity.SetCooldownAttack(entity.stateAttackSelected);
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

    public Vector2 GetDir()
    {
        return (entity.target.position - spawnPoint.position).normalized;
    }
}
