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

        // �J�[�\����\������Ȃ�Œ������
        if (draw)
            Cursor.lockState = CursorLockMode.None;

        // �J�[�\������\���Ȃ��ʒ����ɌŒ�
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
}
