using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

// �ݒ��ʂ̔ėp�{�^��
public class OptionsButton : Button
{
    [SerializeField] private int num = 0; // �J�ڐ�̐ݒ荀�ڂ̔ԍ�

    private OptionsController opCon;

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
        // �ݒ��ʂ̊K�w��ύX
        opCon.loot = (OptionsController.Loot)Enum.ToObject(typeof(OptionsController.Loot), num);
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

        opCon = OptionsController.instance;
    }
}
