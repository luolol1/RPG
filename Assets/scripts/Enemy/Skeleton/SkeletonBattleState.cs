using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform player;
    private Enemy_Skeleton enemy;
    private int moveDir;
    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.Instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(enemy.isPlayerDetected())
        {
            StateTimer = enemy.BattleTime;
            if(enemy.isPlayerDetected().distance<=enemy.attackDistance)
            {
                if(CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }
        }
        else
        {
            if (StateTimer < 0 || Vector2.Distance(enemy.transform.position,player.position)>7)
                stateMachine.ChangeState(enemy.moveState);
        }
            
        
        
        
        if (player.position.x >= enemy.transform.position.x)
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDir = -1;
        enemy.SetVelocity(enemy.MoveSpeed* moveDir,enemy.rb.velocity.y);
    }
    private bool CanAttack()
    {
        if(Time.time-enemy.LastAttackTime>=enemy.attackCoolDown)
        {
            //enemy.LastAttackTime = Time.time;
            return true;
        }
            
        else 
            return false;
    }
}
