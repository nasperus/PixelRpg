using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;

    private string animBoolName;

    protected bool trigerCalled;
    protected float stateTimer;
    
    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        trigerCalled = false;
    }

    public virtual void Exit()
    {
        
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }
    
    
}
