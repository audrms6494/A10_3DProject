using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionSlider : OptionUI
{
    public TMP_Text Title;
    public Slider SliderOption;
    public TMP_Text Data;

    private float preValue;
    private Action<float> _apply;
    
    public void Initialize(string title, float fontSize, float initValue, Action<float> apply)
    {
        Title.text = title;
        Title.fontSize = fontSize;
        preValue = initValue;
        _apply = apply;
        InitValue(initValue);
    }

    private void InitValue(float value)
    {
        SliderOption.value = value;
        Data.text = value.ToString("F2");
    }

    public override void Apply()
    {
        _apply?.Invoke(SliderOption.value);
    }

    public override void Discard()
    {
        _apply?.Invoke(preValue);
        SliderOption.value = preValue;
        Data.text = preValue.ToString("F2");
    }

    public void OnValueChanged(float newValue)
    {
        Data.text = newValue.ToString("F2");
        Apply();
    }

    public override void Refresh(float fontSize)
    {
        Title.fontSize = fontSize;
        Data.fontSize = fontSize;
    }
}
