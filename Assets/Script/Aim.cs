using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    void Update()
    {
        // ���˃{�^��2�������ꂽ��
        if (Input.GetAxisRaw("Fire2") > 0)
        {
            // ���Ԃ̗�����X���[�ɂ���
            Time.timeScale = 0.1f;
        }
        // ���˃{�^��2��������ĂȂ��Ȃ�
        else if (Input.GetAxisRaw("Fire2") == 0)
        {
            // ���Ԃ̗�������ɖ߂�
            Time.timeScale = 1f;
        }
    }
}
