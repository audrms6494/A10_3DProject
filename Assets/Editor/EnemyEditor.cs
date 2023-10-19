using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    private PatrolPatterns patternType;

    public override void OnInspectorGUI()
    {
        Enemy enemy = (Enemy)target;
        if (enemy.PatrolPattern != null )
        {
            switch (enemy.PatrolPattern)
            {
                case CirclePatrol : patternType = PatrolPatterns.CirclePatrol;
                    break;
                case RepeatPatrol : patternType = PatrolPatterns.RepeatPatrol;
                    break;
                case StandingPatrol : patternType = PatrolPatterns.StandingPatrol;
                    break;
                case RepeatPatrolWithJump : patternType = PatrolPatterns.RepeatPatrolWithJump;
                    break;
                case StandingPatrolWithJump : patternType = PatrolPatterns.StandingPatrolWithJump;
                    break;
            };
        }
        // 기존 필드 표시
        DrawDefaultInspector();
        if (!EditorApplication.isPlaying)
        {
            PatrolPatterns lastPattern = patternType;
            patternType = (PatrolPatterns)EditorGUILayout.EnumPopup("Patrol Pattern", patternType);
            if (lastPattern == patternType )
                return;
            switch (patternType)
            {
                case PatrolPatterns.CirclePatrol:
                    enemy.PatrolPattern = new CirclePatrol();
                    break;
                case PatrolPatterns.RepeatPatrol:
                    enemy.PatrolPattern = new RepeatPatrol();
                    break;
                case PatrolPatterns.StandingPatrol:
                    enemy.PatrolPattern = new StandingPatrol();
                    break;
                case PatrolPatterns.RepeatPatrolWithJump:
                    enemy.PatrolPattern = new RepeatPatrolWithJump();
                    break;
                case PatrolPatterns.StandingPatrolWithJump:
                    enemy.PatrolPattern = new StandingPatrolWithJump();
                    break;
            }
            EditorUtility.SetDirty(target);
            
        }
    }
}