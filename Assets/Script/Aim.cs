using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �G�C������
public class Aim : MonoBehaviour
{
    [SerializeField] private ScreenController screenController; // ScreenController�^�̕ϐ�

    void Update()
    {
        // �Q�[����ʂȂ�
        if (screenController.screenNum == 0)
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
}
