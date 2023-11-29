using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Const
{
    public static class AppConst
    {
        public static readonly Vector3 DEFAULT_STAGE_SELECT_POS = new Vector3(0.0f, 100.0f, 0.0f);    // �X�e�[�W�I����ʂ̃J�����̏����ʒu
        public static readonly Vector3 DEFAULT_STAGE_SELECT_ANGLE = new Vector3(30.0f, -90.0f, 0.0f); // �X�e�[�W�I����ʂ̃J�����̏����̌���
        public static readonly Vector3 DEFAULT_IN_GAME_POS = new Vector3(5.0f, 1.0f, 0.0f);           // �Q�[����ʂ̃J�����̏����ʒu
        public static readonly Vector3 DEFAULT_IN_GAME_ANGLE = new Vector3(0.0f, -90.0f, 0.0f);       // �Q�[����ʂ̃J�����̏����̌���
        public const float SPEED_MAINTENANCE_RATE = 1.008f;                                          // �v���C���[�̑��x�ێ���
        public const float SPEED_REDUCTION_RATE = 0.95f;                                             // �v���C���[�̑��x������
        public const float SPEED_THRESHOLD = 0.01f;                                                  // �v���C���[�̑��x�̂������l
        public const int SKILL_NUM = 3;                                                              // �X�L���̐�
        public static readonly string[] SKILL_NAME = new string[]                                    // �X�L����
        {
            "SuperCharge",
            "PowerSurge",
            "Huge",
        };
        public static readonly int[] SKILL_ENERGY_USAGE = new int[]                                  // �X�L���̃G�l���M�[�����
        {
            10,
            20,
            100,
        };
        public static readonly float[] SKILL_COOLDOWN = new float[]                                  // �X�L���̃N�[���_�E��
        {
            1.0f,
            1.0f,
            1.0f,
        };
        public static readonly float[] SKILL_EFFECT_TIME = new float[]                               // �X�L���̌��ʎ���
        {
            10.0f,
            10.0f,
            10.0f,
        };
        public const int CHARGE_SPEED_INCREASE_AMOUNT = 1;                                           // �X�[�p�[�`���[�W�g�p���̃`���[�W���x������
        public const int MASS_INCREASE_AMOUNT = 100;                                                 // �p���[�T�[�W�g�p���̎��ʑ�����
        public const float SIZE_INCREASE_RATE = 2.0f;                                                // ���剻�g�p���̃T�C�Y�����{��
        public const float PREDICTION_LINE_START_WIDTH = 0.1f;                                       // �O���\�����̎n�_�̑���
        public const float PREDICTION_LINE_END_WIDTH = 1.0f;                                         // �O���\�����̏I�_�̑���
        public const float SPHERE_RAY_WIDTH = 10.0f;                                                 // ���̂�Ray�̑���
        public const float DEFAULT_TIME_SCALE = 1.0f;                                                // ���Ԃ̑����̏����l
        public const float SLOW_TIME_SCALE = 0.1f;                                                   // �X���[���[�V�������̎��Ԃ̑���
    }
}
