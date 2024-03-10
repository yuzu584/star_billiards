using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEditor;

// �߂�{�^�����Ǘ�
public class BackButton : Button
{
    private int oldScreen = 0; // �O��̃X�N���[��(�߂��̉��)

    // �}�E�X�|�C���^�[����������̏���
    protected override void EnterProcess()
    {
        // �{�^���̃A�j���[�V��������
        BtnAnimProcess(imageStructs, textStructs, true);
    }

    // �}�E�X�|�C���^�[�����ꂽ�Ƃ��̏���
    protected override void ExitProcess()
    {
        // �{�^���̃A�j���[�V��������
        BtnAnimProcess(imageStructs, textStructs, false);
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    protected override void ClickProcess()
    {
        // ��ʔԍ���O�̉�ʂɂ���
        screenController.ScreenNum = oldScreen;
    }

    // �O��̃X�N���[���ԍ����Z�b�g
    void SetOldScreen()
    {
        oldScreen = screenController.oldScreenNum;
    }

    new void OnEnable()
    {
        base.OnEnable();

        // �{�^���̏���������
        BtnInit(imageStructs, textStructs);
    }

    new void Start()
    {
        base.Start();

        // �f���Q�[�g��ǉ�
        screenController.changeScreen += SetOldScreen;
        input.ui_OnNegativeDele += (float value) =>
        {
            if (screenController.ScreenLoot <= 0)
            {
                if(screenController.ScreenNum == 1)
                    screenController.ScreenNum = 0;
                else
                    screenController.ScreenNum = oldScreen;
            }

        };

        SetOldScreen();
    }
}
