using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
        stateMachine, animBoolName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.PlayerVelocity(0, player.rigidbody.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        if(!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.WallSlide);
        
        player.PlayerVelocity(xInput * player.dashSpeed,0);
        
        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);
        

    }
}