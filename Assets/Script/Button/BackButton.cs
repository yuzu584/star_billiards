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
        scrCon.ScreenLoot = 0;
        scrCon.ScreenNum = oldScreen;
    }

    // �O��̃X�N���[���ԍ����Z�b�g
    void SetOldScreen()
    {
        oldScreen = scrCon.oldScreenNum;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // �{�^���̏���������
        BtnInit(imageStructs, textStructs);
    }

    protected override void Start()
    {
        base.Start();

        // �f���Q�[�g��ǉ�
        scrCon.changeScreen += SetOldScreen;
        input.ui_OnNegativeDele += (float value) =>
        {
            // �K�w��0�ȉ����I�u�W�F�N�g���L���Ȃ�
            if ((scrCon.ScreenLoot <= 0) && (this.gameObject.activeInHierarchy))
            {
                //�����Đ�
                if (sound != null)
                    StartCoroutine(sound.Play(ClickSound));

                // �O�̉�ʂɖ߂�
                scrCon.ScreenNum = oldScreen;
            }

        };

        SetOldScreen();
    }
}
