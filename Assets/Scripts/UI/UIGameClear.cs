using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIGameClear : UIBase
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private TMP_Text DragonName;

    public void Initalize(PlayerInput _playerInput, string _dragonName)
    {
        playerInput = _playerInput;
        DragonName.text = _dragonName;
    }

    public void GoBackHome()
    {
        playerInput.actions.Enable();
        //SceneManager.LoadScene("스테이지 선택 씬 이름");
    }

}
