using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ݒ��ʂ̍��ڂ̖��O�̃e�L�X�g�̃f�[�^
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData/ ConfigTopStringData")]
public class ConfigTopStringData : ScriptableObject
{
    public ConfigTopStrings[] strings;
}