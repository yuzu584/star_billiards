using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Œ¾Œê‚ÌŽí—Þ
public enum LanguageType
{
    English,    // ‰pŒê
    Japanese,   // “ú–{Œê
}

[System.Serializable]
public struct Fonts
{
    public LanguageType language;
    public Font font;
}