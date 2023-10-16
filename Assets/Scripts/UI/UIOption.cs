using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIOption : UIBase
{
    private DataManager SaveData { get => GameManager.Data; }
    // Display Option
    private CustomResolution[] _ResolutionList { get => SaveData.ResolutionArray; }
    private int _ResolutionIndex;
    private int _preResolutionIndex;

    private DisplayInfo[] displayInfos { get => SaveData.GetDisplayInfos(); }
    private int _OutputDisplayIndex;
    private int _preOutputDisplayIndex;

    private bool _FullScreenMode;
    private bool _preFullScreenMode;

    // Sound Option
    private AudioVolume _Volume;
    private AudioVolume _preVolume;

    // UI Option
    private float _UISize;
    private float _preUISize;
    private float _FontSize;
    private float _preFontSize;

    // 변경 체크
    private bool _isChanged = false;

    [Header("Slider Prefab")]
    [SerializeField] GameObject _sliderPrefab;
    [Header("Dropdown Prefab")]
    [SerializeField] GameObject _dropdownPrefab;
    [Header("Check Prefab")]
    [SerializeField] GameObject _checkboxPrefab;


    private Slider _MasterVolSlider;
    private TMP_Text _MasterVolTxt;

    private Slider _BGMVolSlider;
    private TMP_Text _BGMVolTxt;

    private Slider _EffectVolSlider;
    private TMP_Text _EffectVolTxt;

    private Slider _UIVolSlider;
    private TMP_Text _UIVolTxt;

    private TMP_Dropdown _ResolutionDropdown;
    private TMP_Dropdown _outputDisplayDropdown;
    private TMP_Dropdown _DisplayMode;

    public void Initialize(Action actAtClose)
    {
        ActAtClose = actAtClose;

        _Volume.Master = SaveData.MasterVolume;
        _Volume.BGM = SaveData.BGMVolume;
        _Volume.Effect = SaveData.EffectVolume;
        _Volume.UI = SaveData.UIVolume;

        _preVolume = _Volume;

        // TODO

        //Refresh();
    }

    public override void Refresh()
    {
        base.Refresh();
        _MasterVolSlider.value = _Volume.Master;
        _MasterVolTxt.text = _Volume.Master.ToString("F2");

        _BGMVolSlider.value = _Volume.BGM;
        _BGMVolTxt.text = _Volume.BGM.ToString("F2");

        _EffectVolSlider.value = _Volume.Effect;
        _EffectVolTxt.text = _Volume.Effect.ToString("F2");

        _UIVolSlider.value = _Volume.UI;
        _UIVolTxt.text = _Volume.UI.ToString("F2");

        _ResolutionDropdown.value = _ResolutionIndex;
        _outputDisplayDropdown.value = _OutputDisplayIndex;
        _DisplayMode.value = _FullScreenMode ? 0 : 1;
    }

    public void SaveOption()
    {
        // Display Option
        SaveData.ActiveDisplay = _OutputDisplayIndex;
        SaveData.CurrentResolutionIndex = _ResolutionIndex;
        SaveData.IsFullScreen = _FullScreenMode;

        _preResolutionIndex = _ResolutionIndex;
        _preOutputDisplayIndex = _OutputDisplayIndex;
        _preFullScreenMode = _FullScreenMode;

        // Sound Option
        SaveData.MasterVolume = _Volume.Master;
        SaveData.BGMVolume = _Volume.BGM;
        SaveData.EffectVolume = _Volume.Effect;
        SaveData.UIVolume = _Volume.UI;

        _preVolume = _Volume;

        // UI Option
        SaveData.UISize = _UISize;
        SaveData.FontSizeMultiplier = _FontSize;

        _preUISize = _UISize;
        _preFontSize = _FontSize;

        // Success message
        var ui = UIManager.ShowUI<UIPopup>();
        if (ui != null)
        {
            ui.Initialize("설정이 저장되었습니다.", null, null, true, 1f);
        }

        _isChanged = false;
    }

    public void CancelOption()
    {
        if (_isChanged)
        {
            _Volume = _preVolume;

            //_FullScreenMode = _preFullScreenMode;
            //SetResolution(_preResolutionIndex);
            //_ResolutionIndex = _ResolutionList.FindIndex(x =>
            //{
            //    if (x.width == _Resolution.width && x.height == _Resolution.height)
            //        return true;
            //    return false;
            //});

            //_OutputDisplay = _preOutputDisplay;
            //_OutputDisplayIndex = _OutputDisplayList.FindIndex(x =>
            //{
            //    if (x.Equals(_preOutputDisplay))
            //        return true;
            //    return false;
            //});
            //SetTargetDisplay(_OutputDisplayIndex);
        }
        else
            SelfCloseUI();
    }

    public void SetMasterVol(float vol)
    {
        _Volume.Master = vol;
        _isChanged = true;
        _MasterVolTxt.text = _Volume.Master.ToString("F2");
    }

    public void SetBGMVol(float vol)
    {
        _Volume.BGM = vol;
        _isChanged = true;
        _BGMVolTxt.text = _Volume.BGM.ToString("F2");
    }

    public void SetEffectVol(float vol)
    {
        _Volume.Effect = vol;
        _isChanged = true;
        _EffectVolTxt.text = _Volume.Effect.ToString("F2");
    }

    public void SetUIVol(float vol)
    {
        _Volume.UI = vol;
        _isChanged = true;
        _UIVolTxt.text = _Volume.UI.ToString("F2");
    }

    public void SetTargetDisplay(int index)
    {
        var cam = Camera.main;
        cam.targetDisplay = index;
        _isChanged = true;
    }

    public void SetResolution(int index)
    {
        //Resolution resolution = _ResolutionList[index];
        //Screen.SetResolution(resolution.width, resolution.height, _FullScreenMode);
        //_Resolution = resolution;
        _isChanged = true;
    }

    public void SetFullScreenMode(int screenMode)
    {
        bool isFullScreen;
        if (screenMode == 0)
            isFullScreen = true;
        else
            isFullScreen = false;
        //Screen.SetResolution(_Resolution.width, _Resolution.height, isFullScreen);
        _FullScreenMode = isFullScreen;
        _isChanged = true;
    }
}
