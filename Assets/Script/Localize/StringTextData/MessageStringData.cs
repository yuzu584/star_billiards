using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��Ƀ|�b�v�A�b�v�ŏo�镶�͂̃e�L�X�g�̃f�[�^
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData/ MessageStringData")]
public class MessageStringData : ScriptableObject
{
    public MessageStrings[] strings;
}