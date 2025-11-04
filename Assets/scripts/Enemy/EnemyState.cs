using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{

    protected Enemy enemyBase;
    protected EnemyStateMachine stateMachine;

    protected string animBoolName;
    protected float StateTimer;
    protected bool TriggerCalled;
    


    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine,string _animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }
    public virtual void Enter()
    {
        enemyBase.anim.SetBool(animBoolName, true);
        TriggerCalled = false;
    }
    public virtual void Update()
    {
        StateTimer -=Time.deltaTime;
    }
    public virtual void Exit() 
    {
        enemyBase.anim.SetBool(animBoolName, false);
        enemyBase.LastAnimBoolName = animBoolName;
    }
    public void AnimationFinishTrigger()
    {
        TriggerCalled = true;
    }

}
