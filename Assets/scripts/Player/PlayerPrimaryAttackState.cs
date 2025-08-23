using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int ComboCounter;//连击次数
    private float LastAttackTime;//最后一次攻击的时间
    private float ComboWindow = 2;//连击间隔时间
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (ComboCounter > 2 || Time.time - LastAttackTime >= ComboWindow)//连击数大于2或者超过连击间隔时间则重置连击数
            ComboCounter = 0;
        player.anim.SetInteger("ComboCounter", ComboCounter);
        float AttackDir = player.facingDirection;
        xInput = Input.GetAxisRaw("Horizontal");
        if (xInput != 0)
            AttackDir = xInput;
        player.SetVelocity(player.AttackMovement[ComboCounter].x * AttackDir, player.AttackMovement[ComboCounter].y);
        stateTimer = 0.1f;
    }

    public override void Exit()
    {
        base.Exit();
        LastAttackTime=Time.time;
        ComboCounter++;
        player.StartCoroutine("BusyFor",0.15f);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            player.SetVelocity(0,0);
        if (TriggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
