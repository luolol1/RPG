using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public bool isBusy { get; private set; }
    [Header("Attack details")]
    public Vector2[] AttackMovement;
    public float CounterAttackDuration;

    [Header("Move info")]
    public float Movespeed;

    [Header("Jump info")]
    public float Jumpforce;
    //public int jumpCount;//跳跃次数
    //public int maxJumpCount = 2;//最大跳跃次数
    [Header("Dash info")]
    public float DashSpeed;
    public float DashDuration;
    public float DashDirection { get; private set; }


    public GameObject Sword { get; private set; }


    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }//相当于FallState
    public PlayerDashState dashState { get; private set; }//冲刺
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerCounterAttack counterAttack { get; private set; }
    public PlayerAimSwordState aimSword { get; private set; }//瞄准剑状态
    public PlayerCatchSwordState catchSword { get; private set; }

    public PlayerBlackholeState blackholeState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttack = new PlayerCounterAttack(this, stateMachine, "CounterAttack");
        aimSword = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSword = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
        blackholeState = new PlayerBlackholeState(this, stateMachine, "Jump");
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.CurrentState.Update();//持续更新玩家
        CheckForDashInput();//希望在任何时刻都能冲刺闪避

        if(Input.GetKeyDown(KeyCode.F))
            SkillManager.Instance.crystal.CanUseSkill();

    }
    public void AnimationTrigger() => stateMachine.CurrentState.AnimationFinishTrigger();
    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;
        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.Instance.dash.CanUseSkill())
        {
            DashDirection = Input.GetAxisRaw("Horizontal");
            if (DashDirection == 0)
                DashDirection = facingDirection;
            stateMachine.ChangeState(dashState);
        }
    }
    public IEnumerator BusyFor(float seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(seconds);
        isBusy = false;
    }

    public void AssignSword(GameObject _Sword)
    {
        Sword = _Sword;
    }
    public void CatchSword()
    {
        stateMachine.ChangeState(catchSword);
        Destroy(Sword);
    }

}
