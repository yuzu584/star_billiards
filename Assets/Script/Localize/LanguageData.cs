using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 言語ごとのデータ
[CreateAssetMenu(menuName = "MyScriptable/Create LanguageData")]
public class LanguageData : ScriptableObject
{
    public Fonts[] fonts;       // 言語ごとのフォント
}