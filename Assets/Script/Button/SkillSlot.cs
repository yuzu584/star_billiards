using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Const;

public class SkillSlot : Button
{
    [SerializeField] private SkillController.SkillType skill = 0;       // �X�L��
    [SerializeField] private Text nameText;                             // �X�L������\���e�L�X�g
    [SerializeField] private Image selectedImage;                       // �X�L���̑I����Ԃ�\���摜

    // instance��������ϐ�
    private SkillController skillCon;
    private SkillSelectUIController skillSelectUICon;
    private SkillSelect skillSelect;

    private Color defaultSelectImageColor = new(255.0f, 255.0f, 255.0f, 0.04f);
    private Color selectedSelectImageColor = new(255.0f, 255.0f, 255.0f, 1.0f);

    // �}�E�X�|�C���^�[����������̏���
    public override void EnterProcess()
    {
        // �{�^���̃A�j���[�V��������
        BtnAnimProcess(imageStructs, textStructs, true);

        // �X�L���̏���`��
        if(skillSelectUICon != null)
            skillSelectUICon.DrawSkillInfo((int)skill);
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
        if (CheckDoubleSelect(skill))
        {
            SetSelectSkill(skill);
        }
    }

    // �X�L������\���e�L�X�g��ݒ�
    void SetNameText()
    {
        nameText.text = AppConst.SKILL_NAME[(int)skill];
    }

    // �X�L���̑I����ԕ\���摜�̐F��ݒ�
    void SetSelectImageColor()
    {
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            if (skillSelect.selectSlot[i] == skill)
            {
                selectedImage.color = selectedSelectImageColor;
                return;
            }
        }

        // �X�L�����Z�b�g����Ă��Ȃ���ΐF�𔖂�����
        selectedImage.color = defaultSelectImageColor;
    }

    // �I�����Ă���X�L���X���b�g��ݒ�
    void SetSelectSkill(SkillController.SkillType st)
    {
        if (skillSelect.count >= AppConst.SKILL_SLOT_AMOUNT)
        {
            skillSelect.InitSelectSlot();
        }
        skillSelect.selectSlot[skillSelect.count] = st;
        ++skillSelect.count;
    }

    // �����X�L����I�����Ă��Ȃ������m
    bool CheckDoubleSelect(SkillController.SkillType st)
    {
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            if (skillSelect.selectSlot[i] == st) { return false; }
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
        skillSelect = SkillSelect.instance;

        // �X�L�����̃e�L�X�g��ݒ�
        SetNameText();
    }

    void Update()
    {
        // �X�L���̑I����ԕ\���摜�̐F��ݒ�
        SetSelectImageColor();
    }
}
