using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Entity
{
    [SerializeField] private EnemyMoveData moveData;
    [SerializeField] private EnemySpawnData spawnData;

    public Boss1_IdleState IdleState {  get; private set; }

    public Boss1_MoveState MoveState { get; private set; }

    public Boss1_SpawnState SpawnState { get; private set; }

    [SerializeField] private Transform spawnPoint;

    protected override void Start()
    {
        base.Start();
        IdleState = new Boss1_IdleState(this, stateMachine, "idle", this);
        MoveState = new Boss1_MoveState(this, stateMachine, "move", moveData, this);
        SpawnState = new Boss1_SpawnState(this, stateMachine, "spawn", spawnPoint, spawnData, this);
        stateMachine.Initiate(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        if (isReady == null) return;
        if (isReady[stateAttackSelected])
        {
            stateMachine.ChangeState(SpawnState);
        }
    }
}
