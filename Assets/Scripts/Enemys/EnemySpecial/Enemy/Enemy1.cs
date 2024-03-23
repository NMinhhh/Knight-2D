using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    [SerializeField] private EnemyMoveData moveData;
    [SerializeField] protected EnemyDeathData deathData;

    public Enemy1_IdleState IdleState { get; private set; }
    public Enemy1_MoveState MoveState { get; private set; }
    public Enemy1_DeathState DeathState { get; private set; }

    public override void Damage(AttackDetail attackDetail)
    {
        base.Damage(attackDetail);
        if (isDead)
        {
            stateMachine.ChangeState(DeathState);
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    protected override void Start()
    {
        base.Start();
        IdleState = new Enemy1_IdleState(this, stateMachine, "idle", this);
        MoveState = new Enemy1_MoveState(this, stateMachine, "move", moveData, this);
        DeathState = new Enemy1_DeathState(this, stateMachine, "dead", deathData);
        stateMachine.Initiate(IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
