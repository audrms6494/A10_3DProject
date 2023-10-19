using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttackSO", menuName = "Characters/EnemyAttackSO")]
public class EnemyAttackSO : EnemySO
{
    [field: SerializeField] public float AttackSpeed { get; private set; } = 1f;
}
