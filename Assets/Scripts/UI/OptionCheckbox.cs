using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionCheckbox : OptionUI
{
    public TMP_Text Title;
    public Toggle Checkbox;

    private bool preValue;
    private Action<bool> _apply;

    public void Initialize(string title, float fontSize, bool initValue, Action<bool> apply)
    {
        Title.text = title;
        Title.fontSize = fontSize;
        preValue = initValue;
        _apply = apply;
        InitValue(initValue);
    }

    private void InitValue(bool value)
    {
        Checkbox.isOn = preValue;
    }

    public override void Apply()
    {
        _apply?.Invoke(Checkbox.isOn);
    }

    public override void Discard()
    {
        _apply?.Invoke(preValue);
    }

    public void OnValueChanged(bool newValue)
    {
        Apply();
    }
}
