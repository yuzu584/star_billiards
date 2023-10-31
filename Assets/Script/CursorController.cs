using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �}�E�X�J�[�\�����Ǘ�
public class CursorController : MonoBehaviour
{
    [SerializeField] ScreenController screenController; // ScreenController�^�̕ϐ�

    void Start()
    {
        // �J�[�\������ʒ����ɌŒ�
        Cursor.lockState = CursorLockMode.Locked;

        // �J�[�\����\��
        Cursor.visible = false;
    }

    void Update()
    {
        // �߂�{�^���������ꂽ��
        if(Input.GetButtonDown("Cancel"))
        {
            // ��ʔԍ��ɂ���ĕ���
            switch (screenController.screenNum)
            {
                case 0: // InGame

                    // �J�[�\������ʒ����ɌŒ�
                    Cursor.lockState = CursorLockMode.Locked;

                    // �J�[�\����\��
                    Cursor.visible = false;
                    break;

                case 1: // Pause

                    // �J�[�\���̌Œ������
                    Cursor.lockState = CursorLockMode.None;

                    // �J�[�\���\��
                    Cursor.visible = true;
                    break;
            }
        }
    }
}
