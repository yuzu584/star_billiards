using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Q�[�����Ŏg�p����萔���܂Ƃ߂����O���
namespace AppConst
{
    public static class Const_Camera
    {
        public static readonly Vector3 DEFAULT_STAGE_SELECT_POS = new (0.0f, 100.0f, 0.0f);                 // �X�e�[�W�I����ʂ̃J�����̏����ʒu
        public static readonly Vector3 DEFAULT_STAGE_SELECT_ANGLE = new (30.0f, -90.0f, 0.0f);              // �X�e�[�W�I����ʂ̃J�����̏����̌���
        public static readonly Vector3 CAMERA_DEFAULT_IN_GAME_POS = new (5.0f, 1.0f, 0.0f);                 // �Q�[����ʂ̃J�����̏����ʒu
        public static readonly Vector3 CAMERA_DEFAULT_IN_GAME_ANGLE = new (0.0f, -90.0f, 0.0f);             // �Q�[����ʂ̃J�����̏����̌���
        public const float CAMERA_DEFAULT_SPEED_RATE = 1.0f;                                                // ���_�ړ����x�̔{���̏����l
        public const float CAMERA_SLOW_SPEED_RATE = 0.5f;                                                   // ���_�ړ����x�ቺ���̔{��
    }

    public static class Const_Player
    {
        public const float SPEED_MAINTENANCE_RATE = 1.008f;                                                 // �v���C���[�̑��x�ێ���
        public const float SPEED_REDUCTION_RATE = 0.95f;                                                    // �v���C���[�̑��x������
        public const float SPEED_THRESHOLD = 0.01f;                                                         // �v���C���[�̑��x�̂������l
        public const float PLAYER_DEFAULT_SPEED = 1.0f;                                                     // �v���C���[�̈ړ����x�̏����l
        public const int DEFAULT_CHARGE_SPEED = 1;                                                          // �`���[�W���x�̏����l
        public const int DEFAULT_BOUNCE_POWER = 100;                                                        // �����͂̏����l
        public static readonly Vector3 PLAYER_DEFAULT_SCALE = new (0.5f, 0.5f, 0.5f);                       // �v���C���[�̃X�P�[���̏����l
        public const float PREDICTION_LINE_START_WIDTH = 0.3f;                                              // �O���\�����̎n�_�̑���
        public const float PREDICTION_LINE_END_WIDTH = 1.0f;                                                // �O���\�����̏I�_�̑���
        public const float SPHERE_RAY_WIDTH = 10.0f;                                                        // ���̂�Ray�̑���
        public static readonly Vector3 PLATER_DEFAULT_POSITION = new (500.0f, 0.0f, 0.0f);                  // �v���C���[�̏����ʒu
    }

    public static class Const_Skill
    {
        public const int SKILL_NUM = 10;                                                                    // �X�L���̐�

        // �X�L���̍\���̔z��
        public static readonly Skill[] SKILLS = new Skill[SKILL_NUM]
        {
            // �g�p�G�l���M�[��, ���ʎ���, �N�[���_�E����ݒ�
            new (20    , 3.0f  , 10.0f ),
            new (20    , 3.0f  , 10.0f ),
            new (80    , 3.0f  , 10.0f ),
            new (100   , 3.0f  , 0.5f  ),
            new (50    , 3.0f  , 0.5f  ),
            new (50    , 3.0f  , 0.5f  ),
            new (100   , 3.0f  , 10.0f ),
            new (50    , 3.0f  , 10.0f ),
            new (20    , 3.0f  , 0.5f  ),
            new (50    , 3.0f  , 0.5f  ),
        };

        public const int CHARGE_SPEED_INCREASE_AMOUNT = 1;                                                  // �X�[�p�[�`���[�W�g�p���̃`���[�W���x������
        public const int BOUNCE_POWER_INCREASE_AMOUNT = 100;                                                // �p���[�T�[�W�g�p���̔����͑�����
        public const float SIZE_INCREASE_RATE = 5.0f;                                                       // ���剻�g�p���̃T�C�Y�����{��
        public static readonly Color32 DEFAULT_SKILL_GAUGE_COLOR = new (255, 255, 255, 100);                // �X�L���Q�[�W�̏����F
        public static readonly Color32 CAN_USE_SKILL_GAUGE_COLOR = new (255, 255, 100, 200);                // �X�L���g�p�\���̃X�L���Q�[�W�̐F
        public const int SKILL_SLOT_AMOUNT = 3;                                                             // �X�L���X���b�g�̐�
    }

    public static class Const_Button
    {
        public static readonly Color32 SKILLSLOT_SELECTIMAGE_DEFAULT_COLOR = new(255, 255, 255, 10);        // �X�L���X���b�g�̑I�����Ă��邩��\���摜�̏����F
        public static readonly Color32 SKILLSLOT_SELECTIMAGE_SELECT_COLOR = new(255, 255, 255, 200);        // �X�L���X���b�g�̑I�����Ă��邩��\���摜�̑I�����̐F
    }

    public static class Const_System
    {
        public const float DEFAULT_TIME_SCALE = 1.0f;                                                       // ���Ԃ̑����̏����l
        public const float SLOW_TIME_SCALE = 0.1f;                                                          // �X���[���[�V�������̎��Ԃ̑���
    }

    // �X�L���̍\����
    public struct Skill
    {
        public int energyUsage;
        public float effectTime;
        public float coolDown;

        // �R���X�g���N�^
        public Skill(int energyUsage, float effectTime, float coolDown)
        {
            this.energyUsage = energyUsage;
            this.effectTime = effectTime;
            this.coolDown = coolDown;
        }
    }
}