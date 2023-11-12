using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move info")]
    public float moveSPeed;
    public float jumpForce;
    [Space]
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
    
    #endregion


    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        
        idleState = new PlayerIdleState(this, stateMachine,"Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        JumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump");

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
    }

    public void PlayerMovement(float xVelocity, float yVelocity)
    {
        rigidbody.velocity = new Vector2(xVelocity, yVelocity);
        
    }
}
