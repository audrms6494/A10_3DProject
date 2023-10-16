using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class DataManager
{
    [Header("Default Sound Options")]
    [SerializeField] private float _masterVol = 0.5f;
    [SerializeField] private float _BGMVol = 0.5f;
    [SerializeField] private float _effectVol = 0.5f;
    [SerializeField] private float _UIVol = 0.5f;
    public float MasterVolume
    {
        get => _masterVol;
        set
        {
            _masterVol = value;
            SoundManager.ChangeVol(eSoundType.Master);
        }
    }
    public float BGMVolume
    {
        get => _BGMVol;
        set
        {
            _BGMVol = value;
            SoundManager.ChangeVol(eSoundType.BGM);
        }
    }
    public float EffectVolume { get => _effectVol; set => _effectVol = value; }
    public float UIVolume { get => _UIVol; set => _UIVol = value; }

    [Header("Default UI Options")]
    [SerializeField][Range(0.5f, 2f)] private float _UISize = 1.0f;
    [SerializeField][Range(0.5f, 2f)] private float _fontSizeMultiplier = 1.0f;
    [SerializeField][Range(10f, 30f)] private float _UIremainTime = 10f;
    public float UISize
    {
        get => _UISize;
        set
        {
            _UISize = value;
            // TODO
            // UI Update하도록 수정.
        }
    }
    public float FontSizeMultiplier
    {
        get => _fontSizeMultiplier;
        set
        {
            _fontSizeMultiplier = value;
            // TODO
            // UI Update하도록 수정.
        }
    }
    public float UIRemainTime
    {
        get => _UIremainTime;
        set => _UIremainTime = value;
    }

    [Header("Defalut Display Options")]
    [SerializeField] private int _activeDisplay;
    [SerializeField] private List<CustomResolution> _customResolutions;
    [SerializeField] private CustomResolution _currentResolution;
    [SerializeField] private bool _isFullScreen;
    /// <summary>
    /// Camera.main의 target display만 바꿀뿐이므로 주의!!
    /// </summary>
    public int ActiveDisplay
    {
        get => _activeDisplay;
        set
        {
            if (value >= 0 && value < DisplayCount)
            {
                _activeDisplay = value;
                Camera.main.targetDisplay = _activeDisplay;
            }
            else
                Debug.LogWarning($"Wrong Display Index({value}). Maximum Index = {DisplayCount - 1}");
        }
    }
    public int DisplayCount
    {
        get
        {
            return Display.displays.Length;
        }
    }
    public CustomResolution[] ResolutionArray { get => _customResolutions.ToArray(); }
    public int CurrentResolutionIndex { get => _customResolutions.FindIndex(x => x == _currentResolution); set => _currentResolution = _customResolutions[value]; }
    public bool IsFullScreen { get => _isFullScreen; set => _isFullScreen = value; }
    /// <summary>
    /// Linux, maxOS, Windows만 지원하므로 주의!!
    /// </summary>
    /// <returns></returns>
    public DisplayInfo[] GetDisplayInfos()
    {
        List<DisplayInfo> displayLayout = new List<DisplayInfo>();
        Screen.GetDisplayLayout(displayLayout);
        return displayLayout.ToArray();
    }
}
