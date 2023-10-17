using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : PlayerAnimations
{
    private static readonly int IsWalk = Animator.StringToHash("IsWalk");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsHit = Animator.StringToHash("IsHit");

    private CharacterHealth _healthSystem;

    protected override void Awake()
    {
        base.Awake();
        _healthSystem = GetComponent<CharacterHealth>();
    }

    private void Move()
    {
        animator.SetBool(IsWalk, true);
    }

    private void Hit()
    {
        animator.SetBool(IsHit, true);
    }
}
