using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 設定画面の項目の名前のテキストのデータ
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData/ ConfigTopStringData")]
public class ConfigTopStringData : ScriptableObject
{
    public ConfigTopStrings[] strings;
}