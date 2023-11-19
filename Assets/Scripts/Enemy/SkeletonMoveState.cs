using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : EnemyState
{
    private EnemySkeleton enemy;
    public SkeletonMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName,EnemySkeleton enemy) : base(enemy, stateMachine, animBoolName)
    {
        this.enemy = enemy;
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
    }
}
