using System;
using UnityEditor;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    private PatrolPatterns patrolPattern;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Enemy enemy = (Enemy)target;
        if (enemy.PatrolPattern != null )
        {
            switch (enemy.PatrolPattern)
            {
                case CirclePatrol : patrolPattern = PatrolPatterns.CirclePatrol;
                    break;
                case RepeatPatrol : patrolPattern = PatrolPatterns.RepeatPatrol;
                    break;
                case StandingPatrol : patrolPattern = PatrolPatterns.StandingPatrol;
                    break;
                case RepeatPatrolWithJump : patrolPattern = PatrolPatterns.RepeatPatrolWithJump;
                    break;
            };
        }
        // 기존 필드 표시
        DrawDefaultInspector();
        if (!EditorApplication.isPlaying)
        {
            PatrolPatterns lastPattern = patrolPattern;
            patrolPattern = (PatrolPatterns)EditorGUILayout.EnumPopup("Patrol Pattern", patrolPattern);
            if (lastPattern == patrolPattern )
                return;

            switch (patrolPattern)
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
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}