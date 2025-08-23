using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //player.jumpCount++;
        player.SetVelocity(rb.velocity.x, player.Jumpforce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //if(player.jumpCount<player.maxJumpCount && Input.GetKeyDown(KeyCode.Space))
        //{
        //    player.SetVelocity(rb.velocity.x, player.Jumpforce);
        //    player.jumpCount++;
        //}
        if (xInput != 0)
            player.SetVelocity(player.Movespeed * xInput * 0.8f, rb.velocity.y);
        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.airState);
    }
}
