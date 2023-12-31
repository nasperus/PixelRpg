using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
        
        if (xInput != 0)
            player.PlayerVelocity(player.moveSPeed *.8f * xInput, rigidbody.velocity.y);
        
        if(player.IsWallDetected())
            stateMachine.ChangeState(player.WallSlide);
    }
}
