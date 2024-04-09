using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// キーの操作のテキストのデータ
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData/ KeyGuideStringData")]
public class KeyGuideStringData : ScriptableObject
{
    public KeyGuideStrings[] strings;
}