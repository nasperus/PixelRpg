using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move info")]
    public float moveSPeed;
    public float jumpForce;
    public float dashSpeed;
    public float dashDuration;
    [HideInInspector] public float dashing;

    [Space] [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
   
    
    #region Components;
    public Animator animator;
    public Rigidbody2D rigidbody { get; private set; }
    #endregion
    
    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    
    #endregion


    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        
        idleState = new PlayerIdleState(this, stateMachine,"Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        JumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
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
        Debug.DrawRay(groundCheck.position, -transform.up * groundCheckDistance,Color.red);
        
    }

    public void PlayerMovement(float xVelocity, float yVelocity)
    {
        rigidbody.velocity = new Vector2(xVelocity, yVelocity);
        bool characteFlip = rigidbody.velocity != Vector2.zero;
        if (characteFlip)
        { 
            transform.localScale = new Vector2(Math.Sign(rigidbody.velocity.x), 1);
        }
 
    }

    public bool IsGroundDetected() =>
        Physics2D.Raycast(groundCheck.position, -transform.up, groundCheckDistance, whatIsGround);
        
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
    
    
}
