using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

// �ݒ��ʂ̃{�^��
public class OptionsButton : Button
{
    [SerializeField] private OptionsController _optionsController;
    [SerializeField] private int num = 0; // �J�ڐ�̐ݒ荀�ڂ̔ԍ�

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
        // �ݒ��ʂ̊K�w��ύX
        _optionsController.loot = (OptionsController.Loot)Enum.ToObject(typeof(OptionsController.Loot), num);
    }

    void OnEnable()
    {
        // �{�^���̏���������
        BtnInit(imageStructs, textStructs);
    }
}
