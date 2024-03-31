using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 画面関係のテキストのデータ
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData/ ScreenStringData")]
public class ScreenStringData : ScriptableObject
{
    public ScreenStrings[] strings;
}