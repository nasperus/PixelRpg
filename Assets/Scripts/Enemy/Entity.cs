using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Space] [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

     
    #region Components;
    public Animator animator;
    public  Rigidbody2D rigidbody { get; private set; }
    #endregion
    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        
    }
    #region Velocity
    public void ZeroVelocity() => GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    
    public void PlayerVelocity(float xVelocity, float yVelocity)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    #endregion
    #region Flip
    public virtual void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }

    public virtual void FlipController(float x)
    {
        if (x > 0 && !facingRight)
            Flip();
        else if (x < 0 && facingRight)
            Flip();
    }
    #endregion
    #region Collision
    public virtual bool IsGroundDetected() =>
        Physics2D.Raycast(groundCheck.position,-transform.up, groundCheckDistance,whatIsGround);

    public virtual bool IsWallDetected() =>
        Physics2D.Raycast(wallCheck.position,Vector2.right * facingDir,wallCheckDistance,whatIsGround);
    #endregion
    


}
