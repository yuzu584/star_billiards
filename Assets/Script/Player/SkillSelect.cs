using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// �X�L���I�����Ǘ�
public class SkillSelect : Singleton<SkillSelect>
{
    public SkillController.SkillType[] selectSlot = new SkillController.SkillType[AppConst.SKILL_SLOT_AMOUNT];  // �I�����Ă���X�L���X���b�g

    private SkillController skillCon;

    // �X�L�����UI���X�V����f���Q�[�g
    public delegate void DrawSkillInfoDele(SkillController.SkillType type);
    public DrawSkillInfoDele DSIdele;

    private void Start()
    {
        skillCon = SkillController.instance;

        // �I�����Ă���X�L���X���b�g�̔z���������
        InitSelectSlot();
    }

    // �I�����Ă���X�L���X���b�g�̔z���������
    public void InitSelectSlot()
    {
        // �X�L���X���b�g�̐��J��Ԃ�
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            // �X�L���� None(���I��) �ɂ���
            selectSlot[i] = SkillController.SkillType.None;
        }
    }

    // �I�������X�L�����m��
    public void SetSelectSlot()
    {
        // �X�L���X���b�g�̐��J��Ԃ�
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)

            // �X�L�����Z�b�g
            skillCon.skillSlot[i] = selectSlot[i];
    }

    // �I�����Ă���X�L���X���b�g��ݒ�
    public void SetSelectSkill(SkillController.SkillType st)
    {
        int count = 0;

        // �X�L���X���b�g�̐��J��Ԃ�
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            // �X�L�����I���̃X���b�g������������
            if (selectSlot[i] == SkillController.SkillType.None)
            {
                // ���������X���b�g�ɃX�L������
                selectSlot[count] = st;
                break;
            }
            count++;
        }
    }

    // �X�L���I��������
    public void CancelSkill(SkillController.SkillType st)
    {
        int count = 0;

        // �X�L���X���b�g�̐��J��Ԃ�
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            // �I������������ׂ��X�L��������������
            if (selectSlot[i] == st)
            {
                // �I��������
                selectSlot[count] = SkillController.SkillType.None;
                break;
            }
            count++;
        }
    }

    // �����X�L����I�����Ă��Ȃ������m
    public bool CheckDoubleSelect(SkillController.SkillType st)
    {
        // �X�L���X���b�g�̐��J��Ԃ�
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            // �I���ς݂Ȃ� false ��Ԃ�
            if (selectSlot[i] == st) { return false; }
        }

        // �I������Ă��Ȃ���� true ��Ԃ�
        return true;
    }

    // �X�L����3�I������������
    public bool CheckNone()
    {
        // �X�L���X���b�g�̐��J��Ԃ�
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            // None ������������ false ��Ԃ�
            if (selectSlot[i] == SkillController.SkillType.None) { return false; }
        }

        // None ��������Ȃ���� true ��Ԃ�
        return true;
    }
}
