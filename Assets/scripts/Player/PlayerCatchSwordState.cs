using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCatchSwordState : PlayerState
{

    public PlayerCatchSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (player.transform.position.x > player.Sword.transform.position.x && player.facingDirection == 1)
            player.Flip();
        else if (player.transform.position.x < player.Sword.transform.position.x && player.facingDirection == -1)
            player.Flip();

        rb.velocity = new Vector2(5f * -player.facingDirection, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (TriggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
