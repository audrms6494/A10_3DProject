using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBaseState : IState
{
    public EnemyStateMachine stateMachine;
    public Enemy enemy;
    public EnemySO enemyData;
    public Animator animator;
    public Rigidbody rigidbody;
    public NavMeshAgent agent;
    public CharacterController controller;

    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        stateMachine = enemyStateMachine;
        enemy = enemyStateMachine.Enemy;
        animator = enemyStateMachine.Enemy.Animator;
        rigidbody = enemyStateMachine.Enemy.Rigidbody;
        agent = enemyStateMachine.Enemy.Agent;
        controller = enemyStateMachine.Enemy.Controller;
        enemyData = enemyStateMachine.Enemy.Data;
    }
    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        
    }

    public virtual void HandleInput()
    {
        
    }
    public virtual void Update()
    {
        VerticalMovementUpdate();
    }
    public virtual void PhysicsUpdate()
    {
        
    }
    protected void StartAnimation(int animationHash)
    {
        animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        animator.SetBool(animationHash, false);
    }
    private void VerticalMovementUpdate()
    {
        if (stateMachine.verticalVelocity < 0 && controller.isGrounded)
        {
            stateMachine.verticalVelocity = stateMachine.Gravity * Time.deltaTime;
        }
        else
        {
            stateMachine.verticalVelocity += stateMachine.Gravity * Time.deltaTime;
        }
    }
}
