using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    private string animBoolName;

    protected float xInput;
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
    }

    public virtual void Exit()
    {
        player.animator.SetBool(animBoolName,false);
    }



}
