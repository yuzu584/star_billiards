using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEditor;

// �߂�{�^�����Ǘ�
public class BackButton : Button
{
    private ScreenController.ScreenType oldScreen = 0; // �O��̃X�N���[��(�߂��̉��)

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
        // ��ʂ�O�̉�ʂɂ���
        scrCon.ScreenLoot = 0;
        scrCon.Screen = oldScreen;
    }

    // �O��̃X�N���[���ԍ����Z�b�g
    void SetOldScreen()
    {
        oldScreen = scrCon.oldScreen;
    }

    // ��ʂ�߂�
    void Back(float v)
    {
        // �K�w��0�ȉ����I�u�W�F�N�g���L���Ȃ�
        if ((scrCon.ScreenLoot <= 0) && (gameObject.activeInHierarchy))
        {
            //�����Đ�
            if (sound != null)
                StartCoroutine(sound.Play(ClickSound));

            // �O�̉�ʂɖ߂�
            scrCon.Screen = oldScreen;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();

        // �f���Q�[�g��ǉ�
        scrCon.changeScreen += SetOldScreen;
        input.ui_OnNegativeDele += Back;

        SetOldScreen();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        scrCon.changeScreen -= SetOldScreen;
        input.ui_OnNegativeDele -= Back;
    }
}
