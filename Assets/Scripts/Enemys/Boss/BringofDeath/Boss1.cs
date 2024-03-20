using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Entity
{
    [SerializeField] private EnemyMoveData moveData;

    public Boss1_IdleState IdleState {  get; private set; }

    public Boss1_MoveState MoveState { get; private set; }

    protected override void Start()
    {
        base.Start();
        IdleState = new Boss1_IdleState(this, stateMachine, "idle", this);
        MoveState = new Boss1_MoveState(this, stateMachine, "move", moveData, this);
        stateMachine.Initiate(IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
