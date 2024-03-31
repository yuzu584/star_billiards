using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// システムのテキストのデータ
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData/ SystemStringData")]
public class SystemStringData : ScriptableObject
{
    public SystemStrings[] strings;
}