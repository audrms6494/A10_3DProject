using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSoundType
{
    Master,
    BGM,
    Effect,
    UI,
}

public enum eUIType
{
    Popup,
}

[System.Serializable]
public struct CustomResolution
{
    public int Width;
    public int Height;

    public static bool operator ==(CustomResolution left, CustomResolution right)
    {
        if (left.Width == right.Width && left.Height == right.Height)
            return true;
        return false;
    }
    public static bool operator !=(CustomResolution left, CustomResolution right)
    {
        if (left.Width == right.Width || left.Height == right.Height)
            return false;
        return true;
    }
}

[Serializable]
public struct CustomSize
{
    public float Width;
    public float Height;
}

[Serializable]
public struct AudioVolume
{
    public float Master;
    public float BGM;
    public float Effect;
    public float UI;
}