using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected Entity entity;
    protected StateMachine stateMachine;
    protected string isBoolName;
    protected float startTime;
    protected bool isFinishAnimation;

    public State(Entity entity, StateMachine stateMachine, string isBoolName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.isBoolName = isBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        isFinishAnimation = false;
        DoCheck();
        entity.anim.SetBool(isBoolName, true);
    }

    public virtual void Exit() 
    {
        entity.anim.SetBool(isBoolName, false);
    }

    public virtual void LogicUpdate() { }
    public virtual void PhysicUpdate() => DoCheck();

    public virtual void DoCheck() { }
    
    public virtual void TriggerAnimation() { }

    public virtual void FinishAnimation() => isFinishAnimation = true;
}
