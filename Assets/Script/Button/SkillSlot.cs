using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Const;

public class SkillSlot : Button
{
    [SerializeField] private int skillNum = 0;     // �X�L���ԍ�
    [SerializeField] private Text nameText;        // �X�L������\���e�L�X�g
    [SerializeField] private Text selectNumText;   // �X�L���̑I����������\���e�L�X�g

    // instance��������ϐ�
    private SkillController skillCon;
    private SkillSelectUIController skillSelectUICon;

    // �}�E�X�|�C���^�[����������̏���
    public override void EnterProcess()
    {
        // �{�^���̃A�j���[�V��������
        BtnAnimProcess(imageStructs, textStructs, true);

        // �X�L���̏���`��
        if(skillSelectUICon != null)
            skillSelectUICon.DrawSkillInfo(skillNum);
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
        // ���ɑI�����ꂽ�X�L���łȂ���΁A�I�����Ă���X�L���X���b�g��ݒ�
        if (CheckDoubleSelect(skillNum))
        {
            SetSelectSlot(skillNum);
        }
    }

    // �X�L������\���e�L�X�g��ݒ�
    void SetNameText()
    {
        nameText.text = AppConst.SKILL_NAME[skillNum];
    }

    // �X�L���̑I����������\���e�L�X�g��ݒ�
    void SetSelectNumText()
    {
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            if (skillCon.selectSlot[i] == skillNum)
            {
                selectNumText.enabled = true;
                selectNumText.text = (i + 1).ToString("0");
                return;
            }
        }

        // �X�L�����Z�b�g����Ă��Ȃ���΃e�L�X�g���\��
        selectNumText.enabled = false;
    }

    // �I�����Ă���X�L���X���b�g��ݒ�
    void SetSelectSlot(int num)
    {
        if (skillCon.count >= AppConst.SKILL_SLOT_AMOUNT)
        {
            skillCon.InitSelectSlot();
        }
        skillCon.selectSlot[skillCon.count] = num;
        ++skillCon.count;
    }

    // �����X�L����I�����Ă��Ȃ������m
    bool CheckDoubleSelect(int num)
    {
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            if (skillCon.selectSlot[i] == num) { return false; }
        }
        return true;
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

        skillCon = SkillController.instance;
        skillSelectUICon = SkillSelectUIController.instance;

        // �X�L�����̃e�L�X�g��ݒ�
        SetNameText();
    }

    void Update()
    {
        // �X�L���̑I����������\���e�L�X�g��ݒ�
        SetSelectNumText();
    }
}
