using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;
using static SkillController;

// �X�L���I�����Ǘ�
public class SkillSelect : Singleton<SkillSelect>
{
    public SkillController.SkillType[] selectSlot = new SkillController.SkillType[AppConst.SKILL_SLOT_AMOUNT];  // �I�����Ă���X�L���X���b�g
    public int count = 0;                                                                                       // �X�L���X���b�g��I�������񐔂��J�E���g

    private SkillController skillCon;

    private void Start()
    {
        skillCon = SkillController.instance;

        // �I�����Ă���X�L���X���b�g�̔z���������
        InitSelectSlot();
    }

    // �I�����Ă���X�L���X���b�g�̔z���������
    public void InitSelectSlot()
    {
        selectSlot[0] = SkillType.SuperCharge;
        selectSlot[1] = SkillType.PowerSurge;
        selectSlot[2] = SkillType.Huge;

        count = 0;
    }

    // �I�������X�L�����Z�b�g
    public void SetSelectSlot()
    {
        // �X�L�����Z�b�g
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
            skillCon.skillSlot[i] = selectSlot[i];

        count = AppConst.SKILL_SLOT_AMOUNT;
    }
}
