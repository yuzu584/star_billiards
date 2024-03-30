using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;
using UnityEngine.UI;

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
        switch (type)
        {
            case SkillController.SkillType.SuperCharge: skillName.text = localize.GetString(StringGroup.SkillName, StringType.SuperCharge); break;
            case SkillController.SkillType.PowerSurge: skillName.text = localize.GetString(StringGroup.SkillName, StringType.PowerSurge); break;
            case SkillController.SkillType.Huge: skillName.text = localize.GetString(StringGroup.SkillName, StringType.Huge); break;
            case SkillController.SkillType.GravityWave: skillName.text = localize.GetString(StringGroup.SkillName, StringType.GravityWave); break;
            case SkillController.SkillType.Frieze: skillName.text = localize.GetString(StringGroup.SkillName, StringType.Frieze); break;
            case SkillController.SkillType.GrapplingHook: skillName.text = localize.GetString(StringGroup.SkillName, StringType.GrapplingHook); break;
            case SkillController.SkillType.Slow: skillName.text = localize.GetString(StringGroup.SkillName, StringType.Slow); break;
            case SkillController.SkillType.InertialControl: skillName.text = localize.GetString(StringGroup.SkillName, StringType.InertialControl); break;
            case SkillController.SkillType.Blink: skillName.text = localize.GetString(StringGroup.SkillName, StringType.Blink); break;
            case SkillController.SkillType.TeleportAnchor: skillName.text = localize.GetString(StringGroup.SkillName, StringType.TeleportAnchor); break;
            default: break;
        }

        switch (type)
        {
            case SkillController.SkillType.SuperCharge: effectDetails.text = localize.GetString(StringGroup.SkillDetails, StringType.SuperChargeDetails); break;
            case SkillController.SkillType.PowerSurge: effectDetails.text = localize.GetString(StringGroup.SkillDetails, StringType.PowerSurgeDetails); break;
            case SkillController.SkillType.Huge: effectDetails.text = localize.GetString(StringGroup.SkillDetails, StringType.HugeDetails); break;
            case SkillController.SkillType.GravityWave: effectDetails.text = localize.GetString(StringGroup.SkillDetails, StringType.GravityWaveDetails); break;
            case SkillController.SkillType.Frieze: effectDetails.text = localize.GetString(StringGroup.SkillDetails, StringType.FriezeDetails); break;
            case SkillController.SkillType.GrapplingHook: effectDetails.text = localize.GetString(StringGroup.SkillDetails, StringType.GrapplingHookDetails); break;
            case SkillController.SkillType.Slow: effectDetails.text = localize.GetString(StringGroup.SkillDetails, StringType.SlowDetails); break;
            case SkillController.SkillType.InertialControl: effectDetails.text = localize.GetString(StringGroup.SkillDetails, StringType.InertialControlDetails); break;
            case SkillController.SkillType.Blink: effectDetails.text = localize.GetString(StringGroup.SkillDetails, StringType.BlinkDetails); break;
            case SkillController.SkillType.TeleportAnchor: effectDetails.text = localize.GetString(StringGroup.SkillDetails, StringType.TeleportAnchorDetails); break;
            default: break;
        }

        cost.text = AppConst.SKILL_ENERGY_USAGE[(int)type].ToString("0");
        effectTime.text = AppConst.SKILL_EFFECT_TIME[(int)type].ToString("0") + "s";
        coolDown.text = AppConst.SKILL_COOLDOWN[(int)type].ToString("0") + "s";
    }
}