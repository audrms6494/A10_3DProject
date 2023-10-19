using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;
using UnityEngine.UIElements;

public class CirclePatrol : IPatrolPattern
{
    public enum RotateDirection
    {
        ClockWise = 1,
        CounterClockWise = -1
    }
    private EnemyBaseState baseState;
    private NavMeshAgent agent;
    private CharacterController controller;
    private EnemyStateMachine stateMachine;
    public Transform Center;
    public float Radius = 5f;
    public RotateDirection rotateDirection;
    private float angle = 0f;
    private float angleIncreaseValue;
    private Vector3 destination;
    private Vector3 currentPosition;
    private bool isMovingToCircle;

    public void SetBaseState(EnemyBaseState baseState)
    {
        this.baseState = baseState;
        agent = baseState.agent;
        controller = baseState.controller;
        stateMachine = baseState.stateMachine;
    }
    public void StartPattern()
    {
        angleIncreaseValue = agent.speed / (2 * Mathf.PI * Radius) * 360f;
        if (isOnCircle())
            CalculateOnCircleAngle();
    }

    public void StopPattern()
    {
    }

    public void UpdatePattern()
    {
        CircleMove();
    }
    public void PhysicsUpdate()
    {
    }
    private void CircleMove()
    {
        bool onCircle = isOnCircle();
        if (!onCircle)
        {
            destination = Center.position;
            if (agent.destination != destination)
                agent.SetDestination(destination);
            isMovingToCircle = true;
            return;
        }
        else
        {
            if (isMovingToCircle)
            {
                isMovingToCircle = false;
                agent.SetPath(new NavMeshPath());
                CalculateOnCircleAngle();
            }
        }
        float deltaAngle = (int)rotateDirection * angleIncreaseValue * Time.deltaTime;
        angle += deltaAngle;
        if (angle > 360f)
        {
            angle -= 360f;
        }
        else if (angle < -360f)
        {
            angle += 360f;
        }
        Vector3 centerPoint = Center.position;
        float x = Radius * Mathf.Sin(Mathf.Deg2Rad * (angle + deltaAngle));
        float z = Radius * Mathf.Cos(Mathf.Deg2Rad * (angle + deltaAngle));
        destination = new Vector3(centerPoint.x + x, 0, centerPoint.z + z);
        currentPosition = agent.transform.position;
        currentPosition.y = 0;
        Vector3 moveDirection = destination - currentPosition;
        agent.Move(moveDirection);
        agent.transform.rotation = Quaternion.RotateTowards(agent.transform.rotation, Quaternion.LookRotation(moveDirection, Vector3.up), agent.angularSpeed * Time.deltaTime);
    }
    private bool isOnCircle()
    {
        Vector3 position = agent.transform.position;
        position = position - Center.position;
        float bias = 0.5f;

        return position.x * position.x + position.z * position.z - Radius * Radius <= bias;
    }
    private void CalculateOnCircleAngle()
    {
        Vector3 direction = agent.transform.position - Center.position;
        angle = Vector3.Angle(Center.forward, direction);
        if (direction.x < 0)
            angle = -angle;
    }
}
