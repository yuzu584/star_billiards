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
    Screen              = 000,  // ��ʊ֌W
    StageName           = 010,  // �X�e�[�W��
    Mission             = 020,  // �~�b�V������
    ConfigTop           = 030,  // �ݒ��ʂ̍��ڂ̖��O
    ConfigContent       = 031,  // �ݒ��ʂŐݒ�\�ȍ��ڂ̖��O
    SkillName           = 040,  // �X�L����
    SkillParameter      = 041,  // �X�L���̃p�����[�^�[(����G�l���M�[����ʎ��ԂȂ�)
    SkillDetails        = 042,  // �X�L���̌���
    Player              = 050,  // �v���C���[�֘A
    Message             = 060,  // ��Ƀ|�b�v�A�b�v�ŏo�镶��
    System              = 100,  // �V�X�e��
    None                = 101,
}

// ������̎��
// �v�f���������Ă������悤�ɐ��l���w��
public enum StringType
{
    // ���
    PleaseAnyKey        = 000,  // �^�C�g����ʂɂ���e�L�X�g
    StageSelect         = 001,  // �X�e�[�W�I��
    Options             = 002,  // �ݒ�
    SkillSelect         = 003,  // �X�L���I��
    ExitGame            = 004,  // �Q�[���I��
    Pause               = 005,  // �|�[�Y
    ReturnToGame        = 006,  // �Q�[���ĊJ
    ReturnToMainMenu    = 007,  // ���C�����j���[�ɖ߂�

    // �X�e�[�W��
    Stage1              = 100,
    Stage2              = 101,
    Stage3              = 102,
    Stage4              = 103,
    Stage5              = 104,

    // �~�b�V������
    DestroyPlanet       = 200,  // �f����j�󂵂�
    ReachTheGoal        = 201,  // �S�[���ɂ��ǂ蒅��

    // �ݒ���_Top
    GamePlay            = 300,  // �Q�[���v���C
    Video               = 301,  // �r�f�I
    Audio               = 302,  // �I�[�f�B�I
    KeyConfig           = 303,  // �L�[�R���t�B�O
    Language            = 304,  // ����

    // �ݒ荀��

    // �X�L����
    SuperCharge         = 500,  // �X�[�p�[�`���[�W
    PowerSurge          = 501,  // �p���[�T�[�W
    Huge                = 502,  // ���剻
    GravityWave         = 503,  // �d�͔g
    Frieze              = 504,  // �t���[�Y
    GrapplingHook       = 505,  // �O���b�v�����O�t�b�N
    Slow                = 506,  // �X���[
    InertialControl     = 507,  // �C���i�[�V�����R���g���[��
    Blink               = 508,  // �u�����N
    TeleportAnchor      = 509,  // �e���|�[�g�A���J�[

    // �X�L���̃p�����[�^�[
    EnergyCost          = 530,  // �G�l���M�[�����
    EffectTime          = 531,  // ���ʎ���
    CoolDown            = 532,  // �N�[���_�E��
    EffectDetails       = 533,  // �X�L���̌���

    // �X�L���̌���
    SuperChargeDetails      = 560,  // �X�[�p�[�`���[�W�̌���
    PowerSurgeDetails       = 561,  // �p���[�T�[�W�̌���
    HugeDetails             = 562,  // ���剻�̌���
    GravityWaveDetails      = 563,  // �d�͔g�̌���
    FriezeDetails           = 564,  // �t���[�Y�̌���
    GrapplingHookDetails    = 565,  // �O���b�v�����O�t�b�N�̌���
    SlowDetails             = 566,  // �X���[�̌���
    InertialControlDetails  = 567,  // �C���i�[�V�����R���g���[���̌���
    BlinkDetails            = 568,  // �u�����N�̌���
    TeleportAnchorDetails   = 569,  // �e���|�[�g�A���J�[�̌���

    // �v���C���[�֘A
    Charge              = 600,  // �`���[�W

    // ��Ƀ|�b�v�A�b�v�ŏo�镶��
    WasDecided          = 700,  // �m�肵�܂���
    PleaseSelect3Skills = 701,  // �X�L����3�I��ł��Ȃ��Ƃ��̃e�L�X�g
    ExitGameText        = 702,  // �Q�[���I�����̃e�L�X�g

    // �V�X�e��
    OK                  = 1000, // ����
    Cancel              = 1001, // �L�����Z��
    Apply               = 1002, // �m��
    Reset               = 1003, // ���Z�b�g
    Start               = 1004, // �X�^�[�g

    None                = 1100,
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
