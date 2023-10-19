using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dragon : MonoBehaviour
{
    public string dragonName;
    [SerializeField] private PlayerInput playerInput;

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.Confined;
            var ui = UIManager.ShowUI<UIGameClear>();
            ui.Initalize(playerInput, dragonName);
        }
    }
}
