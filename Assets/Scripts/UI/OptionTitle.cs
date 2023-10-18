using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionTitle : OptionUI
{
    public TMP_Text Title;

    public void Initialize(string title, float fontSize)
    {
        Title.text = title;
        Title.fontSize = fontSize;
    }
}
