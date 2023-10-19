using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterHealth : MonoBehaviour
{
    // 테스트용
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private int maxHealth = 100;
    private int health;
    public event Action OnDie;

    public bool IsDead => health == 0;


    private void Start()
    {
        health = maxHealth;
        OnDie += Die;
    }

    public void TakeDamage(int damage)
    {
        if (health == 0) return;

        health = Mathf.Max(health - damage, 0);

        if (health == 0)
            OnDie?.Invoke();

        Debug.Log(health);
    }

    public void Die()
    {
        Cursor.lockState = CursorLockMode.Confined;
        playerInput.actions.Disable();
    }
}
