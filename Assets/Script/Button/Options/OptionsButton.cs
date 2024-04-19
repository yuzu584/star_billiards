using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ݒ��ʂ̔ėp�{�^��
public class OptionsButton : Button
{
    [SerializeField] private int loot; // �J�ڐ�̐ݒ荀��

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

        // �ݒ��ʂ̊K�w��ύX
        opCon.SwitchLoot(loot);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();

        opCon = OptionsController.instance;
    }
}
