using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;

    private string animBoolName;

    protected bool trigerCalled;
    protected float stateTimer;
    
    public EnemyState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName)
    {
        this.enemyBase = enemyBase;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        trigerCalled = false;
        enemyBase.animator.SetBool(animBoolName,true);
    }

    public virtual void Exit()
    {
        enemyBase.animator.SetBool(animBoolName,false);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }
    
    
}
