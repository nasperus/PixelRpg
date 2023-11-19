using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    private string animBoolName;
    protected float xInput;
    protected float yInput;
    protected Rigidbody2D rigidbody;
    protected float stateTimer;
    protected bool trigerCalled;
    private readonly int yVelocity = Animator.StringToHash("yVelocity");
   
    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        
    }
    
    public virtual void Enter()
    {
       player.animator.SetBool(animBoolName,true);
       rigidbody = player.GetComponent<Rigidbody2D>();
       trigerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.animator.SetFloat(yVelocity, player.GetComponent<Rigidbody2D>().velocity.y);
        
    }

    public virtual void Exit()
    {
        player.animator.SetBool(animBoolName,false);
       
    }

    public virtual void AnimationFinishTrigger()
    {
        trigerCalled = true;
    }

}
