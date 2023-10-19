using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] public EnemySO Data { get; private set; }
    [field: SerializeField] public EnemyAnimationData AnimationData { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public CharacterController Controller { get; private set; }
    public CharacterHealth CharacterHealth { get; private set; }
    [field: SerializeField] public EnemyStateMachine stateMachine { get; private set; }
    public CharacterHealth Target { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    [SerializeField] private readonly bool isAttackable;
    [SerializeField] private readonly bool isChasable;
    [SerializeReference] public IPatrolPattern PatrolPattern;

    void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        AnimationData = new EnemyAnimationData();
        Rigidbody = GetComponent<Rigidbody>();
        Controller = GetComponent<CharacterController>();
        CharacterHealth = GetComponent<CharacterHealth>();
        Agent = GetComponent<NavMeshAgent>();
        stateMachine = new EnemyStateMachine(this);
        Init();
    }

    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterHealth>();
        stateMachine.ChangeState(stateMachine.PatrolState);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    private void Init()
    {
        Agent.speed = Data.MoveSpeed;
        Agent.angularSpeed = Data.RotateSpeed;
        Agent.acceleration = Data.Acceleration;
    }
}