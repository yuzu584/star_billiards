using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// スキル選択を管理
public class SkillSelect : Singleton<SkillSelect>
{
    public SkillController.SkillType[] selectSlot = new SkillController.SkillType[AppConst.SKILL_SLOT_AMOUNT];  // 選択しているスキルスロット

    private SkillController skillCon;

    // スキル情報UIを更新するデリゲート
    public delegate void DrawSkillInfoDele(SkillController.SkillType type);
    public DrawSkillInfoDele DSIdele;

    private void Start()
    {
        skillCon = SkillController.instance;

        // 選択しているスキルスロットの配列を初期化
        InitSelectSlot();
    }

    // 選択しているスキルスロットの配列を初期化
    public void InitSelectSlot()
    {
        // スキルスロットの数繰り返す
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            // スキルを None(未選択) にする
            selectSlot[i] = SkillController.SkillType.None;
        }
    }

    // 選択したスキルを確定
    public void SetSelectSlot()
    {
        // スキルスロットの数繰り返す
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)

            // スキルをセット
            skillCon.skillSlot[i] = selectSlot[i];
    }

    // 選択しているスキルスロットを設定
    public void SetSelectSkill(SkillController.SkillType st)
    {
        int count = 0;

        // スキルスロットの数繰り返す
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            // スキル未選択のスロットが見つかったら
            if (selectSlot[i] == SkillController.SkillType.None)
            {
                // 見つかったスロットにスキルを代入
                selectSlot[count] = st;
                break;
            }
            count++;
        }
    }

    // スキル選択を解除
    public void CancelSkill(SkillController.SkillType st)
    {
        int count = 0;

        // スキルスロットの数繰り返す
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            // 選択を解除するべきスキルが見つかったら
            if (selectSlot[i] == st)
            {
                // 選択を解除
                selectSlot[count] = SkillController.SkillType.None;
                break;
            }
            count++;
        }
    }

    // 同じスキルを選択していないか検知
    public bool CheckDoubleSelect(SkillController.SkillType st)
    {
        // スキルスロットの数繰り返す
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            // 選択済みなら false を返す
            if (selectSlot[i] == st) { return false; }
        }

        // 選択されていなければ true を返す
        return true;
    }

    // スキルを3つ選択したか判定
    public bool CheckNone()
    {
        // スキルスロットの数繰り返す
        for (int i = 0; i < AppConst.SKILL_SLOT_AMOUNT; i++)
        {
            // None が見つかったら false を返す
            if (selectSlot[i] == SkillController.SkillType.None) { return false; }
        }

        // None が見つからなければ true を返す
        return true;
    }
}
