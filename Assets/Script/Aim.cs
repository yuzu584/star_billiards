using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    // �J�����ړ��J�E���g
    public static float cameraMoveCount = 0;

    void Update()
    {
        // ���˃{�^��2�������ꂽ��
        if (Input.GetAxisRaw("Fire2") > 0)
        {
            // ���Ԃ̗�����X���[�ɂ���
            Time.timeScale = 0.1f;

            // �J�����ړ��J�E���g�𑝉�
            cameraMoveCount += 0.1f;
        }
        // ���˃{�^��2��������ĂȂ��Ȃ�
        else if (Input.GetAxisRaw("Fire2") == 0)
        {
            // ���Ԃ̗�������ɖ߂�
            Time.timeScale = 1f;

            // �J�����ړ��J�E���g������
            cameraMoveCount -= 0.1f;
        }

        if (cameraMoveCount < 0)
        {
            cameraMoveCount = 0;
        }
        else if(cameraMoveCount > 1)
        {
            cameraMoveCount = 1;
        }
    }
}
