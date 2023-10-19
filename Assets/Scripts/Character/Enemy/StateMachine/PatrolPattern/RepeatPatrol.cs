using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class RepeatPatrol : IPatrolPattern
{
    private EnemyBaseState baseState;
    private NavMeshAgent agent;
    private CharacterController controller;
    public Transform RepeatPoint1;
    public Transform RepeatPoint2;

    public void SetBaseState(EnemyBaseState baseState)
    {
        this.baseState = baseState;
        agent = baseState.agent;
        controller = baseState.controller;
    }
    public void StartPattern()
    {
        
    }

    public void StopPattern()
    {
    }

    public void UpdatePattern()
    {
        RepeatMove();
    }

    public void PhysicsUpdate()
    {
    }

    private void RepeatMove()
    {
        if (agent.remainingDistance < 0.1f)
        {
            if (controller.bounds.Contains(RepeatPoint1.position))
            {
                agent.SetDestination(RepeatPoint2.position);
            }
            else
            {
                agent.SetDestination(RepeatPoint1.position);
            }
        }
    }
}
