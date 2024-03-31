using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ステージ名のテキストのデータ
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData/ StageNameStringData")]
public class StageNameStringData : ScriptableObject
{
    public StageNameStrings[] strings;
}