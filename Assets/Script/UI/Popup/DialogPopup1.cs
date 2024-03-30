using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �_�C�A���O�|�b�v�A�b�v1
public class DialogPopup1 : PopupParent
{
    [SerializeField] private Button1 okBtn, cancelBtn;  // �{�^��1�̃R���|�[�l���g

    private  Action action;                             // OK �{�^�����������Ƃ��Ɏ��s���鏈��
    public Action Action
    {
        get { return action; }
        set {
            SetBtnAction(okBtn, value);                 // action ������Ƀ{�^���̃A�N�V�������ݒ肷��
            action = value;
        }
    }

    private Focus focus;

    protected override void Start()
    {
        base.Start();

        focus ??= Focus.instance;

        // �L�����Z���{�^���̏����̓|�b�v�A�b�v�̔j��
        Action a = () =>
        {
            scrCon.ScreenLoot = scrCon.oldScreenLoot;
            focus.SetFocusBtn(focus.oldLootFocusBtn);
        };

        SetBtnAction(cancelBtn, a);

        // Negative �{�^������������L�����Z���{�^���̃A�N�V�������N����
        input.ui_OnNegativeDele += CancelBtnProcess;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        input.ui_OnNegativeDele -= CancelBtnProcess;
    }

    // �L�����Z���{�^���̏������s��
    void CancelBtnProcess(float f)
    {
        cancelBtn.PlayBtnSound(cancelBtn.ClickSound);
        cancelBtn.ClickProcess();
    }

    // �{�^���̃A�N�V������ݒ�
    void SetBtnAction(Button1 btn1, Action action)
    {
        btn1.action = action;
    }

    // �{�^���� ScreenAndLoot ��ݒ�
    public void SetScreenAndLoot(ScreenController.ScreenType scrType, int loot)
    {
        okBtn.scrAndLoot.scrType = scrType;
        okBtn.scrAndLoot.scrLoot = loot;
        cancelBtn.scrAndLoot.scrType = scrType;
        cancelBtn.scrAndLoot.scrLoot = loot;
    }

    // �|�b�v�A�b�v�̏���
    public override IEnumerator Process(string text, Transform parentT, int num)
    {
        index = num;

        popupMana ??= PopupManager.instance;
        focus ??= Focus.instance;

        // �e��ݒ�
        popupMana.popupContent[(int)popupType].instance[index].transform.SetParent(parentT, false);

        // �e�L�X�g��ݒ�
        popupText.text = text;

        // �L�����Z���{�^�����t�H�[�J�X
        focus.SetFocusBtn(cancelBtn);

        yield return null;
    }
}
