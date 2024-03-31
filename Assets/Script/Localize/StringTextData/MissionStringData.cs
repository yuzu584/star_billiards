using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ミッションのテキストのデータ
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData/ MissionStringData")]
public class MissionStringData : ScriptableObject
{
    public MissionStrings[] strings;
}