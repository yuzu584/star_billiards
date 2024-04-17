using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 言語の種類
public enum LanguageType
{
    English,    // 英語
    Japanese,   // 日本語
}

[System.Serializable]
public struct Fonts
{
    public LanguageType language;
    public Font font;
}