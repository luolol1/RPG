using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    private Vector2 MousePosition;
    public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SkillManager.Instance.sword.SetUpActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", 0.1f);
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, 0);

        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (player.transform.position.x > MousePosition.x && player.facingDirection == 1)
            player.Flip();
        else if(player.transform.position.x < MousePosition.x && player.facingDirection == -1)
            player.Flip();

        if (Input.GetKeyUp(KeyCode.Mouse1))
            stateMachine.ChangeState(player.idleState);
    }
}
