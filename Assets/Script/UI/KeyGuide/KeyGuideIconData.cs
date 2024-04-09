using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �L�[����K�C�h�̉摜���Ǘ�
[CreateAssetMenu(menuName = "MyScriptable/Create KeyGuideIconData")]
public class KeyGuideIconData : ScriptableObject
{
    public KeyTypeAndIcons[] keyTypeAndIcons;
}

// �A�C�R�����܂Ƃ߂�\����
[System.Serializable]
public struct KeyIcons
{
    public Sprite KeybordAndMouse;      // �L�[�{�[�g�ƃ}�E�X�g�p���̃A�C�R��
    public Sprite GamePad;              // �Q�[���p�b�h�g�p���̃A�C�R��
}

// ���͂̎�ނɉ������A�C�R�����i�[����\����
[System.Serializable]
public struct KeyTypeAndIcons
{
    public EnumKeyGuide keyGuideType;
    public KeyIcons KeyIcons;
}