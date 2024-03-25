using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �}�E�X�J�[�\�����Ǘ�
public class CursorController : MonoBehaviour
{
    [SerializeField] private ScreenData scrData;             // Inspector��ScreenData���w��

    private ScreenController scrCon;
    private bool draw;                                       // �J�[�\����\�����邩�ǂ���

    private void Start()
    {
        scrCon = ScreenController.instance;

        scrCon.changeScreen += SwitchCursorState;
    }

    // �J�[�\���̏�Ԃ�؂�ւ�
    void SwitchCursorState()
    {
        // ���݂̃X�N���[���ԍ��ŃJ�[�\����\�����邩�ǂ������߂�
        draw = scrData.screenList[(int)scrCon.Screen].drawCursol;

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
