using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�N���[���̃��X�g
[CreateAssetMenu(menuName = "MyScriptable/Create ScreenData")]
public class ScreenData : ScriptableObject
{
    public List<ScreenDataContent> screenList;
}

// �K�w�̃N���X
[System.Serializable]
public class LootStr
{
    public string name;             // ���O
    public GameObject screenObj;    // ��ʂ̃I�u�W�F�N�g
}

// �X�N���[���̃��X�g
[System.Serializable]
public class ScreenDataContent
{
    public string screenName;       // ���O
    public GameObject screenObj;    // ��ʂ̃I�u�W�F�N�g
    public LootStr[] loot;          // �K�w���Ƃ̏��
    public bool drawCursol;         // �J�[�\����\�����邩
    public float timeScale = 1.0f;  // ���Ԃ�����鑬��
    public bool drawStage = false;  // �X�e�[�W��`�悷�邩
    public bool enterAnim = false;  // ���̉�ʂɑJ�ڂ������ɃA�j���[�V�������s����
    public bool exitAnim = false;   // ���̉�ʂ���J�ڂ������ɃA�j���[�V�������s����
    public int inputType;           // InputSystem�̂ǂ�ActionMap�̓��͂��󂯎�邩
    public bool resetFov;           // ���̉�ʂɑJ�ڂ����Ƃ��Ɏ���p�����Z�b�g���邩
}
