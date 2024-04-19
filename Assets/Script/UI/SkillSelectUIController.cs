using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AppConst;
using UnityEngine.UI;
using System;

// スキル選択画面のUIを管理
public class SkillSelectUIController : MonoBehaviour
{
    [SerializeField] private Text skillName, cost, effectTime, coolDown, effectDetails;

    private SkillSelect skillSelect;
    private Localize localize;

    void Start()
    {
        skillSelect = SkillSelect.instance;
        localize = Localize.instance;

        // フォントを設定
        skillName.font = localize.GetFont();
        effectDetails.font = localize.GetFont();

        // スキル情報UIを更新するデリゲートに登録
        skillSelect.DSIdele += DrawSkillInfo;

        // スキルの情報を描画
        DrawSkillInfo(0);
    }

    private void OnDestroy()
    {
        skillSelect.DSIdele -= DrawSkillInfo;
    }

    // スキルの情報を描画
    void DrawSkillInfo(SkillController.SkillType type)
    {
        // スキル名のテキストを更新
        skillName.text = localize.GetString("skill_name", type.ToString());

        // スキルの効果のテキストを更新
        effectDetails.text = localize.GetString("skill_details", type.ToString());

        // スキルのコストのテキストを更新
        cost.text = Const_Skill.SKILLS[(int)type].energyUsage.ToString("0");

        // スキルの効果時間のテキストを更新
        effectTime.text = Const_Skill.SKILLS[(int)type].effectTime.ToString("0") + "s";

        // スキルのクールダウンのテキストを更新
        coolDown.text = Const_Skill.SKILLS[(int)type].coolDown.ToString("0") + "s";
    }
}