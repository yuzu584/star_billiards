using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �߂�{�^�����Ǘ�
public class BackButton : Button
{
    public ScreenController.ScreenType oldScreen = 0;   // �߂��̉��
    public bool oldScreenAutoSet = true;                // �߂��̉�ʂ������Őݒ肷�邩

    public Action action;

    // �}�E�X�|�C���^�[����������̏���
    public override void EnterProcess()
    {
        base.EnterProcess();
    }

    // �}�E�X�|�C���^�[�����ꂽ�Ƃ��̏���
    public override void ExitProcess()
    {
        base.ExitProcess();
    }

    // �N���b�N���ꂽ�Ƃ��̏���
    public override void ClickProcess()
    {
        base.ClickProcess();

        // �{�^�������b�N����Ă����珈�����s�킸�I��
        if (lockButton) return;

        action?.Invoke();
    }

    // �O��̃X�N���[���ԍ����Z�b�g
    void SetOldScreen()
    {
        if (oldScreenAutoSet)
            oldScreen = scrCon.oldScreen;
    }

    // ��ʂ�߂�
    void Back(float v)
    {
        action?.Invoke();
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

        if(action == null)
        {
            action = () =>
            {
                // �K�w��0�ȉ����I�u�W�F�N�g���L���Ȃ�
                if ((scrCon.ScreenLoot <= 0) && (gameObject.activeInHierarchy))
                {
                    //�����Đ�
                    if (sound != null)
                        PlayBtnSound(BtnSounds.ClickSound);

                    // �O�̉�ʂɖ߂�
                    scrCon.Screen = oldScreen;
                }
            };
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        scrCon.changeScreen -= SetOldScreen;
        input.ui_OnNegativeDele -= Back;
    }
}
