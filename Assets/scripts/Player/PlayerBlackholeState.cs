using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class PlayerBlackholeState : PlayerState
{
    private float flytime = 0.3f;
    private bool SkiilUsed;
    private float defaultGravity;
    public PlayerBlackholeState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = flytime;
        defaultGravity = rb.gravityScale;
        rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = defaultGravity;
        player.MakeTransparent(false);
        SkiilUsed = false;
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer >0)
            rb.velocity = new Vector2(0, 15f);
        if(stateTimer <=0)
        {
            rb.velocity = new Vector2(0, -0.1f);
            if(!SkiilUsed)
            {
                if (SkillManager.Instance.blackhole.CanUseSkill())
                    SkiilUsed = true;
                
            }
        }
        if(SkillManager.Instance.blackhole.BlackholeFinished())
            stateMachine.ChangeState(player.airState);
    }
}
