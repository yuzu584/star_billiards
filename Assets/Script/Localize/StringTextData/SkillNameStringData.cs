using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スキル名のテキストのデータ
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData/ SkillNameStringData")]
public class SkillNameStringData : ScriptableObject
{
    public SkillNameStrings[] strings;
}