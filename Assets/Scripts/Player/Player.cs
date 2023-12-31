using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: Entity
{
    [Header("Attack details")] 
    public Vector2[] attackMovement; 
    public bool isBusy { get; private set; }
    
    [Header("Move info")]
    public float moveSPeed;
    public float jumpForce;

    [Header("Dash info")] 
    [SerializeField] private float dashCooldown;
    private float dashTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }
    
    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState WallSlide { get; private set; }
    public PlayerWallJumpState walljump { get; private set; }
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    #endregion

    private PlayerState playerState;
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        
        idleState = new PlayerIdleState(this, stateMachine,"Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        JumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        WallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        walljump  = new PlayerWallJumpState(this, stateMachine, "WallJump");
        
        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
    }
    
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
      
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        Debug.DrawRay(groundCheck.position, -transform.up * groundCheckDistance, Color.red);
        CheckForDashInput();
    }

    public IEnumerator BusyFor(float seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(seconds);
        isBusy = false;
    }
    
    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;
        
        dashTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer < 0)
        {
            dashTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir != 0)
                stateMachine.ChangeState(dashState);
        }
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    

  

}
