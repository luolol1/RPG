using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //if (player.jumpCount < player.maxJumpCount && Input.GetKeyDown(KeyCode.Space))
        //{
        //    stateMachine.ChangeState(player.jumpState);
        //}
        if (xInput != 0)
            player.SetVelocity(player.Movespeed * xInput * 0.8f, rb.velocity.y);
        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlide);
        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
            //player.jumpCount = 0;
        }
    }
}
