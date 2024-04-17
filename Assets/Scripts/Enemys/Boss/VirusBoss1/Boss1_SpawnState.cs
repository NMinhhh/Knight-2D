using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_SpawnState : EnemySpawnState
{
    private Boss1 boss;
    private GameObject GO;
    public Boss1_SpawnState(Entity entity, StateMachine stateMachine, string isBoolName, Transform spawnPoint, EnemySpawnData data, Boss1 boss) : base(entity, stateMachine, isBoolName, spawnPoint, data)
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
        //entity.anim.SetBool("startSkill", false);
    }

    public override void FinishAnimation()
    {
        base.FinishAnimation();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //if (isFinishAnimation && !isStartSpawn)
        //{
        //    isFinishAnimation = false;
        //    entity.anim.SetBool("startSkill", true);
        //    isStartSpawn = true;
        //}
        //else if(isStartSpawn && !isFinishAnimation)
        //{
        if(amountOfSpawnGO > 0)
        {
            if(Time.time > startTime + data.cooldown)
            {
                SpawnBullet();
                startTime = Time.time;
                amountOfSpawnGO--;
            }
        }
        else
        {
            stateMachine.ChangeState(boss.IdleState);
            //entity.anim.SetBool("startSkill", false);
        }
        //}
        //else if (isFinishAnimation)
        //{
        //    stateMachine.ChangeState(boss.IdleState);
        //}
    }

    void SpawnBullet()
    {
        float angle = Mathf.Atan2(GetDir().y,GetDir().x) * Mathf.Rad2Deg;
        GO = GameObject.Instantiate(data.spawnGO, spawnPoint.position, Quaternion.Euler(0, 0, angle));
        //Vector3 localScale = Vector3.one;
        //if (angle > 90 || angle < -90)
        //{
        //    localScale.y = -1;
        //}
        //else
        //{
        //    localScale.y = 1;
        //}
        //GO.transform.localScale = localScale;
        Projectile script = GO.GetComponent<Projectile>();
        script.CreateBullet(data.damage, data.speed, data.timeLife);
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
