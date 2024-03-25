using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;
using static SkillController;

// スキル選択を管理
public class SkillSelect : Singleton<SkillSelect>
{
    public SkillController.SkillType[] selectSlot = new SkillController.SkillType[AppConst.SKILL_SLOT_AMOUNT];  // 選択しているスキルスロット
    public int count = 0;                                                                                       // スキルスロットを選択した回数をカウント

    private SkillController skillCon;

    private void Start()
    {
        skillCon = SkillController.instance;

        // 選択しているスキルスロットの配列を初期化
        InitSelectSlot();
    }

    // 選択しているスキルスロットの配列を初期化
    public void InitSelectSlot()
    {
        selectSlot[0] = SkillType.SuperCharge;
        selectSlot[1] = SkillType.PowerSurge;
        selectSlot[2] = SkillType.Huge;

        count = 0;
    }

    // 選択したスキルをセット
    public void SetSelectSlot()
    {
        // スキルをセット
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
            skillCon.skillSlot[i] = selectSlot[i];

        count = AppConst.SKILL_SLOT_AMOUNT;
    }
}
