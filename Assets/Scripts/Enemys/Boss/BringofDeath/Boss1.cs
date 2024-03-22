using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Entity
{
    [SerializeField] private EnemyMoveData moveData;
    [SerializeField] private EnemySpawnData spawnData;
    [SerializeField] private EnemyDashAttackData dashAttackData;
    [SerializeField] private EnemyDeathData deathData;

    public Boss1_IdleState IdleState {  get; private set; }

    public Boss1_MoveState MoveState { get; private set; }

    public Boss1_SpawnState SpawnState { get; private set; }

    public Boss1_DashAttackState DashAttackState { get; private set; }
    public Boss1_DeathState DeathState { get; private set; }

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform attackPoint;

    protected override void Start()
    {
        base.Start();
        IdleState = new Boss1_IdleState(this, stateMachine, "idle", this);
        MoveState = new Boss1_MoveState(this, stateMachine, "move", moveData, this);
        SpawnState = new Boss1_SpawnState(this, stateMachine, "spawn", spawnPoint, spawnData, this);
        DashAttackState = new Boss1_DashAttackState(this, stateMachine, "dashAttack", dashAttackData, attackPoint, this);
        DeathState = new Boss1_DeathState(this, stateMachine, "dead", deathData, this);
        stateMachine.Initiate(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        if (isReady == null || stateMachine.currentState == SpawnState || stateMachine.currentState == DashAttackState) return;

        if (isReady[0])
        {
            stateMachine.ChangeState(SpawnState);
        }else if (isReady[1])
        {
            stateMachine.ChangeState(DashAttackState);
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(attackPoint.position, dashAttackData.radius);
    }

    public override void Damage(AttackDetail attackDetail)
    {
        base.Damage(attackDetail);
        if (isDead)
        {
            stateMachine.ChangeState(DeathState);
        }
    }
}
