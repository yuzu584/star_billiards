using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �e����̃e�L�X�g
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData")]
public class StringTextData : ScriptableObject
{
    public Fonts[] fonts;
    public StringData[] stringData;
}

// ����̎��
public enum LanguageType
{
    English,    // �p��
    Japanese,   // ���{��
}

// ������̎��
// �v�f���������Ă������悤�ɐ��l���w��
public enum StringType
{
    // �^�C�g�����
    PleaseAnyKey = 000,

    // ���C�����j���[
    StageSelect = 100,
    Options = 101,
    SkillSelect = 102,
    ExitGame = 103,

    // �X�e�[�W�I�����
    Stage1 = 200,
    Stage2 = 201,
    Stage3 = 202,
    Stage4 = 203,
    Stage5 = 204,

    // �ݒ���
    GamePlay = 300,
    Video = 301,
    Audio = 302,
    KeyConfig = 303,
    Language = 304,

    // �X�L���I�����
    SuperCharge = 400,
    PowerSurge = 401,
    Huge = 402,
    GravityWave = 403,
    Frieze = 404,
    GrapplingHook = 405,
    Slow = 406,
    InertialControl = 407,
    Blink = 408,
    TeleportAnchor = 409,

    // �Q�[�����

    // �|�[�Y���
    Pause = 600,
    ReturnToGame = 601,
    ReturnToMainMenu = 602,

    // �f�������

    // �X�e�[�W�N���A�E�Q�[���I�[�o�[���

    // ���̑�
    OK = 1000,
    Cancel = 1001,

    None = 2000,
}

// ���ꂲ�Ƃ̍\����
[System.Serializable]
public struct StringData
{
    public StringType type;
    public Strings[] strings;
}

// ������̍\����
[System.Serializable]
public struct Strings
{
    public LanguageType language;
    public string text;
}

[System.Serializable]
public struct Fonts
{
    public LanguageType language;
    public Font font;
}
