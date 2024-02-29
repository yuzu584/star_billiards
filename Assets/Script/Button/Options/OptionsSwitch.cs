using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// �ݒ��ʂ̃X�C�b�`�{�^��
public class OptionsSwitch : Button
{
    [SerializeField] private OptionsController _optionsController;
    [SerializeField] private Text state;                           // �{�^���̏�Ԃ�\���e�L�X�g
    [SerializeField] private string[] stateText;                   // �{�^���̏�Ԃ�\���e�L�X�g�ɃZ�b�g���镶����
    private int nowState = 0;                                      // ���݂̃{�^���̏��

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
        ++nowState;
        if(nowState > (stateText.Length - 1))
            nowState = 0;
        SetStateText();
    }

    void OnEnable()
    {
        // �{�^���̏���������
        BtnInit(imageStructs, textStructs);

        // �{�^���̃e�L�X�g��ݒ�
        SetStateText();
    }

    // �{�^���̃e�L�X�g��ݒ�
    private void SetStateText()
    {
        state.text = stateText[nowState];
    }
}
