using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionDropdown : OptionUI
{
    public TMP_Text Title;
    public TMP_Dropdown DropdownOption;

    private int preValue;
    private Action<int> _apply;

    public void Initialize(string title, float fontSize, int initValue, Action<int> apply)
    {
        Title.text = title;
        Title.fontSize = fontSize;
        preValue = initValue;
        _apply = apply;
        InitValue(initValue);
    }

    private void InitValue(int value)
    {
        DropdownOption.value = value;
    }

    public override void Apply()
    {
        _apply?.Invoke(DropdownOption.value);
    }

    public override void Discard()
    {
        _apply?.Invoke(preValue);
        DropdownOption.value = preValue;
    }

    public void OnValueChanged(int newValue)
    {
        Apply();
    }
}
