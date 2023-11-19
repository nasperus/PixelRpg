using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : EnemyState
{
    private EnemySkeleton enemy;
    public SkeletonIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName,EnemySkeleton enemy) : base(enemyBase, stateMachine, animBoolName)
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
