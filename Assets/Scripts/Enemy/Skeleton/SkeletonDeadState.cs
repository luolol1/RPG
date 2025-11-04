using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeadState : EnemyState
{
    private Enemy_Skeleton enemy;
    public SkeletonDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.anim.SetBool(enemy.LastAnimBoolName, true);
        enemy.cd.enabled = false;

        StateTimer = 0.15f;
    }


    public override void Update()
    {
        base.Update();
        if (StateTimer > 0)
            enemy.rb.velocity = new Vector2(0, 10);
    }
}
