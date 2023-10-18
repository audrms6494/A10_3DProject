using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : PlayerAnimations
{
    private static readonly int IsWalk = Animator.StringToHash("IsWalking");
    private static readonly int IsHit = Animator.StringToHash("IsHit");

    private CharacterHealth _healthSystem;

    protected override void Awake()
    {
        base.Awake();
        _healthSystem = GetComponent<CharacterHealth>();
    }

    // Start is called before the first frame update
    private void Move(Vector2 obj)
    {
        animator.SetBool(IsWalk, obj.magnitude > .5f);
    }

    private void Hit()
    {
        animator.SetBool(IsHit, true);
    }
}
