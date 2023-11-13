using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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

    [Space] [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    
    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;
    
    #region Components;
    public Animator animator;
    public  Rigidbody2D rigidbody { get; private set; }
    #endregion
    
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
    private void Awake()
    {
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
    
    private void Start()
    {
        stateMachine.Initialize(idleState);
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
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
    
#region Velocity
    public void ZeroVelocity() => rigidbody.velocity = new Vector2(0, 0);
    
    public void PlayerVelocity(float xVelocity, float yVelocity)
    {
        rigidbody.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    #endregion
#region Flip
    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }

    public void FlipController(float x)
    {
        if (x > 0 && !facingRight)
            Flip();
        else if (x < 0 && facingRight)
            Flip();
    }
#endregion
#region Collision
    public bool IsGroundDetected() =>
        Physics2D.Raycast(groundCheck.position,-transform.up, groundCheckDistance,whatIsGround);

    public bool IsWallDetected() =>
        Physics2D.Raycast(wallCheck.position,Vector2.right * facingDir,wallCheckDistance,whatIsGround);
#endregion
  

}
