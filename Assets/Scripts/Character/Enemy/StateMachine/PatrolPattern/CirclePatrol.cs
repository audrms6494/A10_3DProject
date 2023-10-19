using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePatrol : IPatrolPattern
{
    private EnemyBaseState baseState;

    public void PhysicsUpdate()
    {
    }

    public void SetBaseState(EnemyBaseState baseState)
    {
        this.baseState = baseState;
    }
    public void StartPattern()
    {
    }

    public void StopPattern()
    {
    }

    public void UpdatePattern()
    {
    }
}
