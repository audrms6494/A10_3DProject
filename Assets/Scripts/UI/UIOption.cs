using System;
using System.Collections.Generic;
using UnityEngine;


public class UIOption : UIBase
{
    private DataManager SaveData { get => GameManager.Data; }

    // 변경 체크
    private bool _isChanged = false;

    // UI Option List
    private List<OptionUI> _OptionList;
    private List<OptionUI> _TitleList;

    [Header("Option Prefabs")]
    [SerializeField] RectTransform _contents;
    [SerializeField] GameObject _titlePrefab;
    [SerializeField] GameObject _sliderPrefab;
    [SerializeField] GameObject _dropdownPrefab;
    [SerializeField] GameObject _checkboxPrefab;

    public void Initialize(Action actAtClose)
    {
        _isChanged = false;
        ActAtClose = actAtClose;
        transform.localScale = Vector3.one * SaveData.UISize;
        if (_OptionList != null && _TitleList != null)
        {
            Refresh();
            return;
        }
        _OptionList = new List<OptionUI>();
        _TitleList = new List<OptionUI>();
        // TODO
        // DIsplay
        var opt = AddOption(eOptionType.Title, _contents);
        (opt as OptionTitle).Initialize("화면 설정", _baseFontSize[0] * SaveData.FontSizeMultiplier);

        opt = AddOption(eOptionType.Dropdown, _contents);
        List<string> optionList = new List<string>();
        foreach (var resolution in SaveData.ResolutionArray)
            optionList.Add(resolution.ToString());
        (opt as OptionDropdown).Initialize("해상도", _baseFontSize[1] * SaveData.FontSizeMultiplier, optionList, SaveData.CurrentResolutionIndex,
            (value) => { if (SaveData.CurrentResolutionIndex != value) _isChanged = true; SaveData.CurrentResolutionIndex = value; });
        

        opt = AddOption(eOptionType.Dropdown, _contents);
        optionList.Clear();
        for (int i = 0; i < SaveData.DisplayCount; i++)
            optionList.Add(i.ToString());
        (opt as OptionDropdown).Initialize("모니터", _baseFontSize[1] * SaveData.FontSizeMultiplier, optionList, SaveData.ActiveDisplay,
            (value) => { if (SaveData.ActiveDisplay != value) _isChanged = true; SaveData.ActiveDisplay = value; });

        opt = AddOption(eOptionType.Checkbox, _contents);
        (opt as OptionCheckbox).Initialize("전체화면", _baseFontSize[1] * SaveData.FontSizeMultiplier, SaveData.IsFullScreen,
            (value) => { if (SaveData.IsFullScreen != value) _isChanged = true; SaveData.IsFullScreen = value; });

        // Sound
        opt = AddOption(eOptionType.Title, _contents);
        (opt as OptionTitle).Initialize("소리 설정", _baseFontSize[0] * SaveData.FontSizeMultiplier);

        opt = AddOption(eOptionType.Slider, _contents);
        (opt as OptionSlider).Initialize("전체 소리 크기", _baseFontSize[1] * SaveData.FontSizeMultiplier, SaveData.MasterVolume,
            (value) => { if (SaveData.MasterVolume != value) _isChanged = true; SaveData.MasterVolume = value; });

        opt = AddOption(eOptionType.Slider, _contents);
        (opt as OptionSlider).Initialize("효과음 크기", _baseFontSize[1] * SaveData.FontSizeMultiplier, SaveData.EffectVolume,
            (value) => { if (SaveData.EffectVolume != value) _isChanged = true; SaveData.EffectVolume = value; ; });

        opt = AddOption(eOptionType.Slider, _contents);
        (opt as OptionSlider).Initialize("UI 소리 크기", _baseFontSize[1] * SaveData.FontSizeMultiplier, SaveData.UIVolume,
            (value) => { if (SaveData.UIVolume != value) _isChanged = true; SaveData.UIVolume = value; });

        opt = AddOption(eOptionType.Slider, _contents);
        (opt as OptionSlider).Initialize("BGM 소리 크기", _baseFontSize[1] * SaveData.FontSizeMultiplier, SaveData.BGMVolume,
            (value) => { if (SaveData.BGMVolume != value) _isChanged = true; SaveData.BGMVolume = value; });

        // UI
        opt = AddOption(eOptionType.Title, _contents);
        (opt as OptionTitle).Initialize("UI 설정", _baseFontSize[0] * SaveData.FontSizeMultiplier);

        opt = AddOption(eOptionType.Slider, _contents);
        (opt as OptionSlider).Initialize("UI 크기 설정", _baseFontSize[1] * SaveData.FontSizeMultiplier, SaveData.UISize,
            (value) => { if (SaveData.UISize != value) _isChanged = true; SaveData.UISize = value; });

        opt = AddOption(eOptionType.Slider, _contents);
        (opt as OptionSlider).Initialize("Font 크기 설정", _baseFontSize[1] * SaveData.FontSizeMultiplier, SaveData.FontSizeMultiplier,
            (value) => { if (SaveData.FontSizeMultiplier != value) _isChanged = true; SaveData.FontSizeMultiplier = value; });

    }

    public override void Refresh()
    {
        base.Refresh();
        // TODO
        transform.localScale = Vector3.one * SaveData.UISize;
        foreach (var item in _OptionList)
        {
            item.transform.localScale = Vector3.one * SaveData.UISize;
            item.Refresh(_baseFontSize[1] * SaveData.FontSizeMultiplier);
        }
        foreach (var item in _TitleList)
        {
            item.Refresh(_baseFontSize[0] * SaveData.FontSizeMultiplier);
        }
    }

    private OptionUI AddOption(eOptionType type, RectTransform root)
    {
        GameObject obj;
        switch (type)
        {
            case eOptionType.Title:
                obj = Instantiate(_titlePrefab, root);
                return AddOptionList<OptionTitle>(obj);
            case eOptionType.Slider:
                obj = Instantiate(_sliderPrefab, root);
                return AddOptionList<OptionSlider>(obj);
            case eOptionType.Dropdown:
                obj = Instantiate(_dropdownPrefab, root);
                return AddOptionList<OptionDropdown>(obj);
            case eOptionType.Checkbox:
                obj = Instantiate(_checkboxPrefab, root);
                return AddOptionList<OptionCheckbox>(obj);
        }
        return null;
    }

    private T AddOptionList<T>(GameObject obj) where T : OptionUI
    {
        var opt = obj.GetComponent<T>();
        if (typeof(T) == typeof(OptionTitle))
            _TitleList.Add(opt);
        else
            _OptionList.Add(opt);
        return opt;
    }

    public void SaveOption()
    {
        // TODO
        foreach (var opt in _OptionList)
        {
            opt.Apply();
        }
        Refresh();
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
            // TODO
            foreach (var opt in _OptionList)
                opt.Discard();
            _isChanged = false;
        }
        else
            SelfHideUI();
    }
}
