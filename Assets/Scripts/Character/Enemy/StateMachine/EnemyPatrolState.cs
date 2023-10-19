using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    public EnemyPatrolState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }
    public override void Enter()
    {
        enemy.PatrolPattern.StartPattern();
    }
    public override void Exit()
    {
        enemy.PatrolPattern.StopPattern();
    }
    public override void Update()
    {
        base.Update();
        enemy.PatrolPattern.UpdatePattern();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.PatrolPattern.PhysicsUpdate();
    }
}
