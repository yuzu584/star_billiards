using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// システムのテキストのデータ
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData/ StarNameStringData")]
public class StarNameStringData : ScriptableObject
{
    public StarNameStrings[] strings;
}