using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunnedState : EnemyState
{
    private Enemy_Skeleton enemy;
    public SkeletonStunnedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.FX.InvokeRepeating("RedColorblink", 0, 0.1f);
        StateTimer = enemy.StunnedDuration;
        enemy.rb.velocity = new Vector2(-enemy.facingDirection * enemy.StunnedDir.x, enemy.StunnedDir.y);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.FX.Invoke("CancelColorChange", 0);
    }

    public override void Update()
    {
        base.Update();
        if (StateTimer < 0)
            stateMachine.ChangeState(enemy.idleState);
    }
}
