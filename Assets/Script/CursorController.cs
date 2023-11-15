using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �}�E�X�J�[�\�����Ǘ�
public class CursorController : MonoBehaviour
{
    [SerializeField] private ScreenData screenData;             // Inspector��ScreenData���w��
    [SerializeField] private ScreenController screenController; // Inspector��ScreenController���w��

    private bool draw; // �J�[�\����\�����邩�ǂ���

    void Update()
    {
        // ���݂̃X�N���[���ԍ��ŃJ�[�\����\�����邩�ǂ������߂�
        draw = screenData.screenList[screenController.screenNum].drawCursol;

        // �J�[�\���̕\����\���؂�ւ�
        Cursor.visible = draw;

        // �J�[�\����\������Ȃ�
        if (draw)
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
