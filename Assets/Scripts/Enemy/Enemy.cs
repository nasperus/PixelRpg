using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Enemy : MonoBehaviour
{
    public Rigidbody2D rigidbody { get; private set; }
    public Animator animator { get; private set; }
    
    public EnemyStateMachine stateMachine { get; private set; }

    private void Awake()
    {
        stateMachine = new EnemyStateMachine();
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }
}
