using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �ݒ��ʂ̃X�C�b�`�{�^��
public class OptionsSwitch : Button
{
    [SerializeField] private Text state;                           // �{�^���̏�Ԃ�\���e�L�X�g
    [SerializeField] private string[] stateText;                   // �{�^���̏�Ԃ�\���e�L�X�g�ɃZ�b�g���镶����

    private int nowState = 0;                                      // ���݂̃{�^���̏��
    private OptionsController opCon;

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

        ++nowState;
        if(nowState > (stateText.Length - 1))
            nowState = 0;
        SetStateText();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // �{�^���̃e�L�X�g��ݒ�
        SetStateText();
    }

    protected override void Start()
    {
        base.Start();

        opCon = OptionsController.instance;
    }

    // �{�^���̏�Ԃ�\���e�L�X�g��ݒ�
    private void SetStateText()
    {
        state.text = stateText[nowState];
    }
}
