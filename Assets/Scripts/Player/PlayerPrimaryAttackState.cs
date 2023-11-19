using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private readonly int counterCombo = Animator.StringToHash("ComboCounter");
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 1;
    public PlayerPrimaryAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;
        
        player.animator.SetInteger(counterCombo,comboCounter);
        
        float attackDir = player.facingDir;
        if (xInput != 0)
            attackDir = xInput;
        
        player.PlayerVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);
        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine(player.BusyFor(.15f));
        comboCounter++;
        lastTimeAttacked = Time.time;

    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            player.ZeroVelocity();
        
        if(trigerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
