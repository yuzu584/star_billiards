using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����̎��
public enum LanguageType
{
    English,    // �p��
    Japanese,   // ���{��
}

[System.Serializable]
public struct Fonts
{
    public LanguageType language;
    public Font font;
}