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
        player.PlayerMovement(0, player.rigidbody.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        player.PlayerMovement(xInput * player.dashSpeed, player.rigidbody.velocity.y);
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }

    }
}