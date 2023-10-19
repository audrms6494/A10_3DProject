using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Characters/Enemy")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] public float MoveSpeed { get; private set; } = 3f;
    [field: SerializeField] public float RotateSpeed { get; private set; } = 180f;
    [field: SerializeField] public float Acceleration { get; private set; } = 8f;
    [field: SerializeField] public int Damage { get; private set; } = 1;
    [field: SerializeField] public float JumpHeight { get; private set; } = 1f;
    
}