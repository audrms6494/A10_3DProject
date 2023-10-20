using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class UIGameClear : UIBase
{
    private DataManager _data { get => GameManager.Data; }

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private TMP_Text FoundText;
    [SerializeField] private TMP_Text DragonName;
    [SerializeField] private TMP_Text DragonName2;
    [SerializeField] private TMP_Text BtnText;

    public void Initalize(PlayerInput _playerInput, string _dragonName)
    {
        RefreshSize();
        playerInput = _playerInput;
        DragonName.text = _dragonName;
    }

    public void GoBackHome()
    {
        playerInput.actions.Enable();
        SceneManager.LoadScene("StartScene");
    }

    public override void RefreshSize()
    {
        base.RefreshSize();
        BtnText.fontSize = _baseFontSize[0] * _data.FontSizeMultiplier;
        FoundText.fontSize = _baseFontSize[1] * _data.FontSizeMultiplier;
        DragonName.fontSize = _baseFontSize[1] * _data.FontSizeMultiplier;
        DragonName2.fontSize = _baseFontSize[1] * _data.FontSizeMultiplier;
    }
}
