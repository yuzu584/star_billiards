using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// �G�C������
public class Aim : MonoBehaviour
{
    [SerializeField] private ScreenController screenController; // Inspector��ScreenController���w��

    void Update()
    {
        // �Q�[����ʂȂ�
        if (screenController.screenNum == 5)
        {
            // ���˃{�^��2�������ꂽ��
            if (Input.GetAxisRaw("Fire2") > 0)
            {
                // ���Ԃ̗�����X���[�ɂ���
                Time.timeScale = AppConst.SLOW_TIME_SCALE;
            }
            // ���˃{�^��2��������ĂȂ��Ȃ�
            else if (Input.GetAxisRaw("Fire2") == 0)
            {
                // ���Ԃ̗�������ɖ߂�
                Time.timeScale = AppConst.DEFAULT_TIME_SCALE;
            }
        }
    }
}
