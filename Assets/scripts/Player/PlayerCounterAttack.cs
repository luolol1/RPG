using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttack : PlayerState
{
    public PlayerCounterAttack(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.CounterAttackDuration;
        player.anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, 0);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.AttackCheck.position, player.AttackRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if(hit.GetComponent<Enemy>().CanbeStunned())
                {
                    stateTimer = 10;//任意一个较大的数，确保弹反动作能够完成。
                    player.anim.SetBool("SuccessfulCounterAttack", true);
                    SkillManager.Instance.clone.CreateCloneOnCounterAttack(hit.transform);
                }
            }
        }
        if(stateTimer <0||TriggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
