using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スキルのパラメーターのテキストのデータ
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData/ SkillParameterStringData")]
public class SkillParameterStringData : ScriptableObject
{
    public SkillParameterStrings[] strings;
}