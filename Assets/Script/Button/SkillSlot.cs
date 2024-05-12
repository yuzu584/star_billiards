using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using AppConst;

public class SkillSlot : Button
{
    [SerializeField] private SkillController.SkillType skill = 0;       // �X�L��
    [SerializeField] private Text nameText;                             // �X�L������\���e�L�X�g
    [SerializeField] private Image selectedImage;                       // �X�L���̑I����Ԃ�\���摜
    [SerializeField] private LocalizeText localizeText;

    private SkillSelect skillSelect;

    // �}�E�X�|�C���^�[����������̏���
    public override void EnterProcess()
    {
        base.EnterProcess();

        skillSelect ??= SkillSelect.instance;

        // �X�L���̏���`��
        skillSelect.DSIdele?.Invoke(skill);
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

        // ���ɑI�����ꂽ�X�L���łȂ���΁A�I�����Ă���X�L���X���b�g��ݒ�
        if (skillSelect.CheckDoubleSelect(skill))
        {
            skillSelect.SetSelectSkill(skill);
        }
        // ���ɑI���ς݂Ȃ�΁A�I������������
        else
        {
            skillSelect.CancelSkill(skill);
        }
    }

    // �X�L���̑I����ԕ\���摜�̐F��ݒ�
    void SetSelectImageColor()
    {
        // �X�L���X���b�g�̐��J��Ԃ�
        for (int i = 0; i < Const_Skill.SKILL_SLOT_AMOUNT; i++)
        {
            // �X�L�����Z�b�g����Ă���ΐF�𖾂邭����
            if (skillSelect.selectSlot[i] == skill)
            {
                selectedImage.color = Const_Button.SKILLSLOT_SELECTIMAGE_SELECT_COLOR;
                return;
            }
        }

        // �X�L�����Z�b�g����Ă��Ȃ���ΐF�𔖂�����
        selectedImage.color = Const_Button.SKILLSLOT_SELECTIMAGE_DEFAULT_COLOR;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // �|��e�L�X�g�̐ݒ�
        localizeText.seet = "skill_name";
        localizeText.dataName = skill.ToString();
        localizeText.SetText();
    }

    protected override void Start()
    {
        base.Start();

        skillSelect ??= SkillSelect.instance;
    }

    void Update()
    {
        // �X�L���̑I����ԕ\���摜�̐F��ݒ�
        SetSelectImageColor();
    }
}
