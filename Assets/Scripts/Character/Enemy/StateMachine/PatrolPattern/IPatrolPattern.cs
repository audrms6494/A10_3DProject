using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PatrolPatterns
{
    CirclePatrol,
    RepeatPatrol,
    StandingPatrol,
    RepeatPatrolWithJump,

}
public interface IPatrolPattern
{
    public void SetBaseState(EnemyBaseState baseState);
    public void StartPattern();
    public void StopPattern();
    public void UpdatePattern();
    public void PhysicsUpdate();
}
