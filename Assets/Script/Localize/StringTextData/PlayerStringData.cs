using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �v���C���[�֘A�̃e�L�X�g�̃f�[�^
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData/ PlayerStringData")]
public class PlayerStringData : ScriptableObject
{
    public PlayerStrings[] strings;
}