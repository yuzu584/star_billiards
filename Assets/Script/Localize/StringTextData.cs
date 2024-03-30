using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �e����̃e�L�X�g
[CreateAssetMenu(menuName = "MyScriptable/Create StringTextData")]
public class StringTextData : ScriptableObject
{
    public Fonts[] fonts;
    public Strings[] strings;
}

// ����̎��
public enum LanguageType
{
    English,    // �p��
    Japanese,   // ���{��
}

// ������̃O���[�v
// �v�f���������Ă������悤�ɐ��l���w��
public enum StringGroup
{
    Screen = 000,
    StageName = 010,
    Mission = 020,
    ConfigTop = 030,
    ConfigContent = 031,
    SkillName = 040,
    Player = 050,
    System = 100,
    None = 101,
}

// ������̎��
// �v�f���������Ă������悤�ɐ��l���w��
public enum StringType
{
    // ���
    PleaseAnyKey = 000,
    StageSelect = 001,
    Options = 002,
    SkillSelect = 003,
    ExitGame = 004,
    Pause = 005,
    ReturnToGame = 006,
    ReturnToMainMenu = 007,

    // �X�e�[�W��
    Stage1 = 100,
    Stage2 = 101,
    Stage3 = 102,
    Stage4 = 103,
    Stage5 = 104,

    // �~�b�V����
    DestroyPlanet = 200,
    ReachTheGoal = 201,

    // �ݒ���_Top
    GamePlay = 300,
    Video = 301,
    Audio = 302,
    KeyConfig = 303,
    Language = 304,

    // �ݒ荀��

    // �X�L����
    SuperCharge = 500,
    PowerSurge = 501,
    Huge = 502,
    GravityWave = 503,
    Frieze = 504,
    GrapplingHook = 505,
    Slow = 506,
    InertialControl = 507,
    Blink = 508,
    TeleportAnchor = 509,

    // �v���C���[�֘A
    Charge = 600,

    // �V�X�e��
    OK = 1000,
    Cancel = 1001,
    Apply = 1002,
    Reset = 1003,
    Start = 1004,

    None = 1100,
}

// ���ꂲ�Ƃ̍\����
[System.Serializable]
public struct StringData
{
    public StringType type;
    public string[] text;
}

[System.Serializable]
public struct Fonts
{
    public LanguageType language;
    public Font font;
}

[System.Serializable]
public struct Strings
{
    public StringGroup group;
    public StringData[] stringData;
}
