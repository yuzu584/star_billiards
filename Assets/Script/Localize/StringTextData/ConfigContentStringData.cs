using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ݒ��ʂŐݒ�\�ȍ��ڂ̃e�L�X�g�̃f�[�^
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData/ ConfigContentStringData")]
public class ConfigContentStringData : ScriptableObject
{
    public ConfigContentStrings[] strings;
}