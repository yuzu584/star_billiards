using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 主にポップアップで出る文章のテキストのデータ
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData/ MessageStringData")]
public class MessageStringData : ScriptableObject
{
    public MessageStrings[] strings;
}