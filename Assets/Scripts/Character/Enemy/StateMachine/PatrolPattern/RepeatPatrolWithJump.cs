using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;

[Serializable]
public class RepeatPatrolWithJump : IPatrolPattern
{
    private EnemyBaseState baseState;
    private NavMeshAgent agent;
    private CharacterController controller;
    private EnemyStateMachine stateMachine;
    private Vector3 currentDestination;
    private Vector3 movementDirection = Vector3.zero;
    private Vector3 jumpStartPosition = Vector3.zero;
    public Transform RepeatPoint1;
    public Transform RepeatPoint2;
    private float lastJumpTime;
    private float inAirTime;
    [Range(0, 5f)] public float JumpDelay = 1f;

    public void SetBaseState(EnemyBaseState baseState)
    {
        this.baseState = baseState;
        agent = baseState.agent;
        controller = baseState.controller;
        stateMachine = baseState.stateMachine;
    }
    public void StartPattern()
    {
        currentDestination = RepeatPoint1.position;
        agent.SetDestination(currentDestination);
        controller.Move(Vector3.down * Time.deltaTime);
    }

    public void StopPattern()
    {
    }

    public void UpdatePattern()
    {
        RepeatMove();
        Jump();
    }

    public void PhysicsUpdate()
    {
    }

    private void RepeatMove()
    {
        if (agent.enabled && agent.remainingDistance < 0.1f)
        {
            if (controller.bounds.Contains(RepeatPoint1.position))
            {
                agent.SetDestination(RepeatPoint2.position);
                currentDestination = agent.destination;
            }
            else
            {
                agent.SetDestination(RepeatPoint1.position);
                currentDestination = agent.destination;
            }
        }

    }
    private void Jump()
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
            agent.SetDestination(currentDestination);
            movementDirection.y = 0;
            agent.velocity = movementDirection;
        }
        if (Time.time - lastJumpTime > JumpDelay && controller.isGrounded)
        {
            stateMachine.Jump();
            movementDirection = agent.velocity;
            movementDirection.y = 0;
            jumpStartPosition = agent.transform.position;
            jumpStartPosition.y = 0;
            agent.enabled = false;
        }
        if (!agent.enabled)
        {
            Vector3 currentPosition = controller.transform.position;
            currentPosition.y = 0;
            Vector3 destination = currentDestination;
            destination.y = 0;
            float distance = Vector3.Distance(currentPosition, destination);
            Vector3 forward = controller.transform.forward;
            forward.y = 0;
            if (Vector3.Angle(forward, destination) > 1f)
            {
                int rotateDirection;
                Quaternion rotation = controller.transform.rotation;
                if (destination.x < 0 && (rotation.eulerAngles.y > 270 || rotation.eulerAngles.y < 90))
                    rotateDirection = -1;
                else if (destination.x < 0 && (rotation.eulerAngles.y < 270 && rotation.eulerAngles.y > 90))
                    rotateDirection = 1;
                else if (destination.x > 0 && (rotation.eulerAngles.y > 270 || rotation.eulerAngles.y < 90))
                    rotateDirection = 1;
                else
                    rotateDirection = -1;
                Quaternion newRotation = Quaternion.Euler(0, rotation.eulerAngles.y + rotateDirection * agent.angularSpeed * Time.deltaTime, 0);
                controller.transform.rotation = newRotation;
            }
            if (distance <= stateMachine.BrakeDistance || Vector3.Angle(movementDirection, destination - currentPosition) > 5f)
            {
                movementDirection = movementDirection - movementDirection.normalized * agent.acceleration * Time.deltaTime;
                Vector3 destinationDirection = destination - jumpStartPosition;

                if (movementDirection.magnitude <= 0.01f)
                    movementDirection = -destinationDirection.normalized * agent.acceleration * Time.deltaTime;

                if (Vector3.Angle(destinationDirection, movementDirection) > 5f)
                {
                    Vector3 point1 = RepeatPoint1.position;
                    point1.y = 0;
                    if (destination == point1)
                    {
                        currentDestination = RepeatPoint2.position;
                    }
                    else
                    {
                        currentDestination = RepeatPoint1.position;
                    }
                }
            }
            else if (agent.velocity.sqrMagnitude < agent.speed * agent.speed)
            {
                movementDirection = movementDirection + movementDirection.normalized * agent.acceleration * Time.deltaTime;
                if (movementDirection.sqrMagnitude > agent.speed * agent.speed)
                {
                    movementDirection = movementDirection * (agent.speed * agent.speed) / movementDirection.sqrMagnitude;
                }
            }
            controller.Move((movementDirection + new Vector3(0, stateMachine.verticalVelocity, 0)) * Time.deltaTime);
        }
        
    }
}
