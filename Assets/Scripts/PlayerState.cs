using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    private string animBoolName;
    protected float xInput;

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
      
    }

    public virtual void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        player.animator.SetFloat(yVelocity, player.rigidbody.velocity.y);
       
    }

    public virtual void Exit()
    {
        player.animator.SetBool(animBoolName,false);
       
    }

}
