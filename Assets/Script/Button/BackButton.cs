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
    public override void EnterProcess()
    {
        // �{�^���̃A�j���[�V��������
        BtnAnimProcess(imageStructs, textStructs, true);
    }

    // �}�E�X�|�C���^�[�����ꂽ�Ƃ��̏���
    public override void ExitProcess()
    {
        // �{�^���̃A�j���[�V��������
        BtnAnimProcess(imageStructs, textStructs, false);
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    public override void ClickProcess()
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
            // �K�w��0�ȉ����I�u�W�F�N�g���L���Ȃ�
            if ((screenController.ScreenLoot <= 0) && (this.gameObject.activeInHierarchy))
            {
                //�����Đ�
                if (sound != null)
                    StartCoroutine(sound.Play(ClickSound));

                // �O�̉�ʂɖ߂�
                screenController.ScreenNum = oldScreen;
            }

        };

        SetOldScreen();
    }
}
