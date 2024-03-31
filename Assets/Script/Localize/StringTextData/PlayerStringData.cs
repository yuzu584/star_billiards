using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤー関連のテキストのデータ
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData/ PlayerStringData")]
public class PlayerStringData : ScriptableObject
{
    public PlayerStrings[] strings;
}