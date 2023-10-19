using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

[Serializable]
public class StandingPatrolWithJump : IPatrolPattern
{
    private EnemyBaseState baseState;
    private NavMeshAgent agent;
    private CharacterController controller;
    private EnemyStateMachine stateMachine;
    private float lastJumpTime;
    private float inAirTime;
    [Range(0, 5f)] public float JumpDelay = 1f;
    public void PhysicsUpdate()
    {
    }

    public void SetBaseState(EnemyBaseState baseState)
    {
        this.baseState = baseState;
        agent = baseState.agent;
        controller = baseState.controller;
        stateMachine = baseState.stateMachine;
    }

    public void StartPattern()
    {
    }

    public void StopPattern()
    {
    }

    public void UpdatePattern()
    {
        if (!controller.isGrounded)
        {
            inAirTime += Time.deltaTime;
        }
        else if (controller.isGrounded && inAirTime > 0.2f)
        {
            lastJumpTime = Time.time;
            inAirTime = 0f;
            agent.enabled = true;
        }
        if (Time.time - lastJumpTime > JumpDelay && controller.isGrounded)
        {
            stateMachine.Jump();
            agent.enabled = false;
        }
        controller.Move(new Vector3(0, stateMachine.verticalVelocity, 0) * Time.deltaTime);
    }
}
