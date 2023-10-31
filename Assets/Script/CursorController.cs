using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �}�E�X�J�[�\�����Ǘ�
public class CursorController : MonoBehaviour
{
    void Start()
    {
        // �}�E�X�J�[�\�����\��
        DrawCursol(false);
    }

    // �}�E�X�J�[�\����\�����͔�\���ɂ���
    public void DrawCursol(bool draw)
    {
        // �J�[�\���̕\����\���؂�ւ�
        Cursor.visible = draw;

        // �J�[�\����\������Ȃ�
        if(draw)
        {
            // �J�[�\���̌Œ������
            Cursor.lockState = CursorLockMode.None;
        }
        // �J�[�\������\���Ȃ�
        else
        {
            // �J�[�\������ʒ����ɌŒ�
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
