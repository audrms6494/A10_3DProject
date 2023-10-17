using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIOption : UIBase
{
    private DataManager SaveData { get => GameManager.Data; }

    // 변경 체크
    private bool _isChanged = false;

    // UI Option List
    private List<OptionUI> _OptionList;

    [Header("Option Prefabs")]
    [SerializeField] RectTransform _contents;
    [SerializeField] GameObject _titlePrefab;
    [SerializeField] GameObject _optionRootPrefab;
    [SerializeField] GameObject _sliderPrefab;
    [SerializeField] GameObject _dropdownPrefab;
    [SerializeField] GameObject _checkboxPrefab;

    public void Initialize(Action actAtClose)
    {
        ActAtClose = actAtClose;
        _OptionList = new List<OptionUI>();
        // TODO
    }

    public override void Refresh()
    {
        base.Refresh();
        // TODO
    }

    private OptionUI AddOption(eOptionType type, RectTransform root)
    {
        GameObject obj;
        switch (type)
        {
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
        _OptionList.Add(opt);
        return opt;
    }

    private OptionTitle AddOptionTitle()
    {
        var obj = Instantiate(_titlePrefab, _contents);
        return obj.GetComponent<OptionTitle>();
    }

    private RectTransform AddOptionRoot()
    {
        var obj = Instantiate(_optionRootPrefab, _contents);
        return obj.GetComponent<RectTransform>();
    }

    public void SaveOption()
    {
        // TODO

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
        }
        else
            SelfCloseUI();
    }
}
