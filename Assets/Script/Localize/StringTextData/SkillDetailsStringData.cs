using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スキルの効果のテキストのデータ
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData/ SkillDetailsStringData")]
public class SkillDetailsStringData : ScriptableObject
{
    public SkillDetailsStrings[] strings;
}