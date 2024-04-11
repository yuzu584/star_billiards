using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Screen                  = 000,  // ��ʊ֌W
    StageName               = 010,  // �X�e�[�W��
    Mission                 = 020,  // �~�b�V����
    ConfigTop               = 030,  // �ݒ��ʂ̍��ڂ̖��O
    ConfigContent           = 031,  // �ݒ��ʂŐݒ�\�ȍ��ڂ̖��O
    SkillName               = 040,  // �X�L����
    SkillParameter          = 041,  // �X�L���̃p�����[�^�[(����G�l���M�[����ʎ��ԂȂ�)
    SkillDetails            = 042,  // �X�L���̌���
    Player                  = 050,  // �v���C���[�֘A
    Message                 = 060,  // ��Ƀ|�b�v�A�b�v�ŏo�镶��
    StarName                = 070,  // ���̖��O
    KeyGuide                = 080,  // �L�[�����̃e�L�X�g
    System                  = 100,  // �V�X�e��
    None                    = 101,
}

// ������̗񋓌^�̍\����
[System.Serializable]
public struct StringEnumStruct
{
    public EnumScreen screen;
    public EnumStageName stageName;
    public EnumMission mission;
    public EnumConfigTop configTop;
    public AppParams.ParamsKey configContent;
    public EnumSkillName skillName;
    public EnumSkillParameter skillParameter;
    public EnumSkillDetails skillDetails;
    public EnumPlayer player;
    public EnumMessage message;
    public EnumStarName starName;
    public EnumKeyGuide keyGuide;
    public EnumSystem system;
}

// ��ʊ֌W�̗񋓌^
public enum EnumScreen
{
    PleaseAnyKey            = 000,  // �^�C�g����ʂɂ���e�L�X�g
    StageSelect             = 001,  // �X�e�[�W�I��
    Options                 = 002,  // �ݒ�
    SkillSelect             = 003,  // �X�L���I��
    ExitGame                = 004,  // �Q�[���I��
    Pause                   = 005,  // �|�[�Y
    ReturnToGame            = 006,  // �Q�[���ĊJ
    ReturnToMainMenu        = 007,  // ���C�����j���[�ɖ߂�
    None                    = 100,
}

// �X�e�[�W���̗񋓌^
public enum EnumStageName
{
    Stage1                  = 000,
    Stage2                  = 001,
    Stage3                  = 002,
    Stage4                  = 003,
    Stage5                  = 004,
    None                    = 100,
}

// �~�b�V�����̗񋓌^
public enum EnumMission
{
    DestroyPlanet           = 000,  // �f����j�󂵂�
    ReachTheGoal            = 001,  // �S�[���ɂ��ǂ蒅��
    None                    = 100,
}

// �ݒ��ʂ̍��ڂ̖��O�̗񋓌^
public enum EnumConfigTop
{
    GamePlay                = 000,  // �Q�[���v���C
    Video                   = 001,  // �r�f�I
    Audio                   = 002,  // �I�[�f�B�I
    KeyConfig               = 003,  // �L�[�R���t�B�O
    Language                = 004,  // ����
    None                    = 100,
}

// �X�L�����̗񋓌^
public enum EnumSkillName
{
    SuperCharge             = 000,  // �X�[�p�[�`���[�W
    PowerSurge              = 001,  // �p���[�T�[�W
    Huge                    = 002,  // ���剻
    GravityWave             = 003,  // �d�͔g
    Frieze                  = 004,  // �t���[�Y
    GrapplingHook           = 005,  // �O���b�v�����O�t�b�N
    Slow                    = 006,  // �X���[
    InertialControl         = 007,  // �C���i�[�V�����R���g���[��
    Blink                   = 008,  // �u�����N
    TeleportAnchor          = 009,  // �e���|�[�g�A���J�[
    None                    = 100,
}

// �X�L���̃p�����[�^�[�̗񋓌^
public enum EnumSkillParameter
{
    EnergyCost              = 000,  // �G�l���M�[�����
    EffectTime              = 001,  // ���ʎ���
    CoolDown                = 002,  // �N�[���_�E��
    EffectDetails           = 003,  // �X�L���̌���
    None                    = 100,
}

// �X�L���̌��ʂ̕��̗͂񋓌^
public enum EnumSkillDetails
{
    SuperCharge             = 000,  // �X�[�p�[�`���[�W�̌���
    PowerSurge              = 001,  // �p���[�T�[�W�̌���
    Huge                    = 002,  // ���剻�̌���
    GravityWave             = 003,  // �d�͔g�̌���
    Frieze                  = 004,  // �t���[�Y�̌���
    GrapplingHook           = 005,  // �O���b�v�����O�t�b�N�̌���
    Slow                    = 006,  // �X���[�̌���
    InertialControl         = 007,  // �C���i�[�V�����R���g���[���̌���
    Blink                   = 008,  // �u�����N�̌���
    TeleportAnchor          = 009,  // �e���|�[�g�A���J�[�̌���
    None                    = 100,
}

// �v���C���[�֘A�̗񋓌^
public enum EnumPlayer
{
    Charge                  = 000,  // �`���[�W
    None                    = 100,
}

// ��Ƀ|�b�v�A�b�v�ŏo�镶�̗͂񋓌^
public enum EnumMessage
{
    WasDecided              = 000,  // �m�肵�܂���
    PleaseSelect3Skills     = 001,  // �X�L����3�I��ł��Ȃ��Ƃ��̃e�L�X�g
    ExitGameText            = 002,  // �Q�[���I�����̃e�L�X�g
    WasDestroyed            = 003,  // �f�����j�󂳂ꂽ�Ƃ��̃��b�Z�[�W
    None                    = 100,
}

// ���̖��O�̗񋓌^
public enum EnumStarName
{
    Sun                     = 000,  // ���z
    Mercury                 = 001,  // ����
    Venus                   = 002,  // ����
    Earth                   = 003,  // �n��
    Mars                    = 004,  // �ΐ�
    Jupiter                 = 005,  // �ؐ�
    Saturn                  = 006,  // �y��
    Uranus                  = 007,  // �V����
    Neptune                 = 008,  // �C����
    BlackHole               = 009,  // �u���b�N�z�[��
    None                    = 100,
}

// �L�[�����̃e�L�X�g�̗񋓌^
public enum EnumKeyGuide
{
    Positive                = 000,  // Positive ����
    Negative                = 001,  // Negative ����
    MoveCursol              = 002,  // �㉺���E����(�J�[�\���ړ�)
    None                    = 100,
}

// �V�X�e���̗񋓌^
public enum EnumSystem
{
    OK                      = 000,  // ����
    Cancel                  = 001,  // �L�����Z��
    Apply                   = 002,  // �m��
    Reset                   = 003,  // ���Z�b�g
    Start                   = 004,  // �X�^�[�g
    Score                   = 005,  // �X�R�A
    HighScore               = 006,  // �n�C�X�R�A
    NewRecord               = 007,  // �V�L�^
    None                    = 100,
}

[System.Serializable]
public struct Fonts
{
    public LanguageType language;
    public Font font;
}

// ��ʊ֌W�̕�����������\����
[System.Serializable]
public struct ScreenStrings
{
    public EnumScreen type;
    public string[] text;
}

// �X�e�[�W���̕�����������\����
[System.Serializable]
public struct StageNameStrings
{
    public EnumStageName type;
    public string[] text;
}

// �~�b�V�����̕�����������\����
[System.Serializable]
public struct MissionStrings
{
    public EnumMission type;
    public string[] text;
}

// �ݒ��ʂ̍��ڂ̖��O�̕�����������\����
[System.Serializable]
public struct ConfigTopStrings
{
    public EnumConfigTop type;
    public string[] text;
}

// �ݒ��ʂŐݒ�\�ȍ��ڂ̖��O�̕�����������\����
[System.Serializable]
public struct ConfigContentStrings
{
    public AppParams.ParamsKey type;
    public string[] text;
}

// �X�L�����̕�����������\����
[System.Serializable]
public struct SkillNameStrings
{
    public EnumSkillName type;
    public string[] text;
}

// �X�L���̃p�����[�^�[�̕�����������\����
[System.Serializable]
public struct SkillParameterStrings
{
    public EnumSkillParameter type;
    public string[] text;
}

// �X�L���̌��ʂ̕�����������\����
[System.Serializable]
public struct SkillDetailsStrings
{
    public EnumSkillDetails type;
    public string[] text;
}

// �v���C���[�֘A�̕�����������\����
[System.Serializable]
public struct PlayerStrings
{
    public EnumPlayer type;
    public string[] text;
}

// ��Ƀ|�b�v�A�b�v�ŏo�镶����������\����
[System.Serializable]
public struct MessageStrings
{
    public EnumMessage type;
    public string[] text;
}

// ���̖��O�̕�����������\����
[System.Serializable]
public struct StarNameStrings
{
    public EnumStarName type;
    public string[] text;
}

// �L�[�����̃e�L�X�g�������\����
[System.Serializable]
public struct KeyGuideStrings
{
    public EnumKeyGuide type;
    public string[] text;
}

// �V�X�e���̕�����������\����
[System.Serializable]
public struct SystemStrings
{
    public EnumSystem type;
    public string[] text;
}