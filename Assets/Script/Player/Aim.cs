using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// �G�C������
public class Aim : MonoBehaviour
{
    private InputController input;

    void Start()
    {
        input = InputController.instance;

        input.game_OnAimDele += Process;
    }

    // �G�C�����̏���
    void Process(float value)
    {
        // �G�C���{�^���������ꂽ��
        if (value > 0)
        {
            // ���Ԃ̗�����X���[�ɂ���
            Time.timeScale = Const_System.SLOW_TIME_SCALE;
        }
        // �G�C���{�^����������ĂȂ��Ȃ�
        else if (value == 0)
        {
            // ���Ԃ̗�������ɖ߂�
            Time.timeScale = Const_System.DEFAULT_TIME_SCALE;
        }
    }
}
