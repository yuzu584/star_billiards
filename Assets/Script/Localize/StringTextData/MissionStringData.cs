using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �~�b�V�����̃e�L�X�g�̃f�[�^
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData/ MissionStringData")]
public class MissionStringData : ScriptableObject
{
    public MissionStrings[] strings;
}