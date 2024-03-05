using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// �G�C������
public class Aim : MonoBehaviour
{
    [SerializeField] private ScreenController screenController; // Inspector��ScreenController���w��
    [SerializeField] private InputController input;             // Inspector��InputController���w��

    private float inputValue; // ���˃{�^��2�̓���

    void Update()
    {
        // ���˃{�^��2�̓��͂��擾
        inputValue = input.Game_Aim;

        // �Q�[����ʂȂ�
        if (screenController.ScreenNum == 5)
        {
            // ���˃{�^��2�������ꂽ��
            if (inputValue > 0)
            {
                // ���Ԃ̗�����X���[�ɂ���
                Time.timeScale = AppConst.SLOW_TIME_SCALE;
            }
            // ���˃{�^��2��������ĂȂ��Ȃ�
            else if (inputValue == 0)
            {
                // ���Ԃ̗�������ɖ߂�
                Time.timeScale = AppConst.DEFAULT_TIME_SCALE;
            }
        }
    }
}
