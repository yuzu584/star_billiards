using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;
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
        skillName.text = localize.GetString_SkillName((EnumSkillName)Enum.ToObject(typeof(EnumSkillName), (int)type));

        // スキルの効果のテキストを更新
        effectDetails.text = localize.GetString_SkillDetails((EnumSkillDetails)Enum.ToObject(typeof(EnumSkillDetails), (int)type));

        // スキルのコストのテキストを更新
        cost.text = AppConst.SKILL_ENERGY_USAGE[(int)type].ToString("0");

        // スキルの効果時間のテキストを更新
        effectTime.text = AppConst.SKILL_EFFECT_TIME[(int)type].ToString("0") + "s";

        // スキルのクールダウンのテキストを更新
        coolDown.text = AppConst.SKILL_COOLDOWN[(int)type].ToString("0") + "s";
    }
}