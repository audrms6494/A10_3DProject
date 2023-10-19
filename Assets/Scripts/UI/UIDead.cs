using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class UIDead : UIBase
{
    [SerializeField] private PlayerInput playerInput;

    public void Initalize(PlayerInput _playerInput)
    {
        playerInput = _playerInput;
    }

    public void ReTry()
    {
        playerInput.actions.Enable();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void End()
    {
        playerInput.actions.Enable();
        //SceneManager.LoadScene("∑π∫ß º±≈√ æ¿");
    }
}
