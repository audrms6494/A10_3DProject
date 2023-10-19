using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.TextCore.Text;

public class CharacterHealth : MonoBehaviour
{
    // 테스트용
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private int maxHealth = 100;
    private int health;
    public event Action OnDie;

    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int dieHash = Animator.StringToHash("Die");
    public bool IsDead => health == 0;


    private void Start()
    {
        health = maxHealth;
        OnDie += Die;
    }

    public void TakeDamage(int damage)
    {
        if (health == 0) return;
        transform.GetChild(0).GetComponent<Animator>().SetTrigger(Hit);
        health = Mathf.Max(health - damage, 0);

        if (health == 0)
        {
            OnDie?.Invoke();
            transform.GetChild(0).GetComponent<Animator>().SetTrigger(dieHash);
        }

        Debug.Log(health);
    }

    public void Die()
    {
        Cursor.lockState = CursorLockMode.Confined;
        playerInput.actions.Disable();
        var ui = UIManager.ShowUI<UIDead>();
        ui.Initalize(playerInput);
    }
}
