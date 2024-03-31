using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 設定画面で設定可能な項目のテキストのデータ
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData/ ConfigContentStringData")]
public class ConfigContentStringData : ScriptableObject
{
    public ConfigContentStrings[] strings;
}