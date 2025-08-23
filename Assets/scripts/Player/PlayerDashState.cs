using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SkillManager.Instance.clone.CreateCloneOnDashEnter();
        stateTimer = player.DashDuration;
        
    }

    public override void Exit()
    {
        base.Exit();
        SkillManager.Instance.clone.CreateCloneOnDashExit();
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.DashSpeed * player.DashDirection, 0);
        if (player.IsWallDetected() && !player.IsGroundDetected())
            stateMachine.ChangeState(player.wallSlide);
        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);
    }
}
