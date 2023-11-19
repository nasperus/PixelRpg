using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
       
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.primaryAttack);
        
        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);
        
        if (Input.GetKey(KeyCode.Space)&& player.IsGroundDetected())
            stateMachine.ChangeState(player.JumpState);
        
        
    }
}
