using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }
    public float Gravity;
    public float MovementSpeedModifier { get; set; } = 1f;
    public EnemyPatrolState PatrolState { get; }

    public EnemyAttackState AttackState { get; }

    private CharacterController controller;
    private float jumpSpeed;
    public float BrakeDistance;
    public float verticalVelocity = 0f;
    
    public EnemyStateMachine(Enemy enemy)
    {
        Enemy = enemy;
        Gravity = Physics.gravity.y;
        PatrolState = new EnemyPatrolState(this);
        AttackState = new EnemyAttackState(this);
        Enemy.PatrolPattern.SetBaseState(PatrolState);
        controller = enemy.Controller;
        CalculateJumpSpeed();
        CalculateBrakeDistance();
    }
    public void Jump()
    {
        if (!controller.isGrounded)
            return;
        verticalVelocity += jumpSpeed;
    }
    private void CalculateJumpSpeed()
    {
        jumpSpeed = -Gravity * Mathf.Sqrt(2 * Enemy.Data.JumpHeight / -Gravity);
    }
    private void CalculateBrakeDistance()
    {
        EnemySO Data = Enemy.Data;
        float BrakeTime = Data.MoveSpeed / Data.Acceleration;
        BrakeDistance = Data.Acceleration * BrakeTime * BrakeTime * 0.5f;
        Enemy.Agent.stoppingDistance = BrakeDistance;
    }
}
