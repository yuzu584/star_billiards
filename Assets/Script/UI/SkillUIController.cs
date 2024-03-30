using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Const;
using Unity.VisualScripting;

// スキルのUIを管理
public class SkillUIController : MonoBehaviour
{
    [SerializeField] private Text skillNameText;
    [SerializeField] private Image skillGauge;

    private Initialize init;
    private SkillController skillCon;
    private Localize localize;

    // スキルのUIを描画
    void DrawSkillUI()
    {
        string skillName;
        switch (skillCon.skillSlot[skillCon.selectSkill])
        {
            case SkillController.SkillType.SuperCharge: skillName = localize.GetString(StringGroup.SkillName, StringType.SuperCharge); break;
            case SkillController.SkillType.PowerSurge: skillName = localize.GetString(StringGroup.SkillName, StringType.PowerSurge); break;
            case SkillController.SkillType.Huge: skillName = localize.GetString(StringGroup.SkillName, StringType.Huge); break;
            case SkillController.SkillType.GravityWave: skillName = localize.GetString(StringGroup.SkillName, StringType.GravityWave); break;
            case SkillController.SkillType.Frieze: skillName = localize.GetString(StringGroup.SkillName, StringType.Frieze); break;
            case SkillController.SkillType.GrapplingHook: skillName = localize.GetString(StringGroup.SkillName, StringType.GrapplingHook); break;
            case SkillController.SkillType.Slow: skillName = localize.GetString(StringGroup.SkillName, StringType.Slow); break;
            case SkillController.SkillType.InertialControl: skillName = localize.GetString(StringGroup.SkillName, StringType.InertialControl); break;
            case SkillController.SkillType.Blink: skillName = localize.GetString(StringGroup.SkillName, StringType.Blink); break;
            case SkillController.SkillType.TeleportAnchor: skillName = localize.GetString(StringGroup.SkillName, StringType.TeleportAnchor); break;
            default: skillName = "null skill"; break;
        }

        float effectTime = AppConst.SKILL_EFFECT_TIME[(int)skillCon.skillSlot[skillCon.selectSkill]];
        float coolDown = AppConst.SKILL_COOLDOWN[(int)skillCon.skillSlot[skillCon.selectSkill]];

        // テキストを現在のスキル名に変更
        skillNameText.text = skillName;

        // 効果時間を描画
        if (skillCon.effectTime > 0)
            skillGauge.fillAmount = skillCon.effectTime / effectTime;

        // 効果時間が経過していたならクールダウンを描画
        else if (skillCon.coolDown > 0)
            skillGauge.fillAmount = (coolDown - skillCon.coolDown) / coolDown;

        // スキルが使用可能かつ黄色ではないなら黄色にする
        if((skillCon.coolDown == 0) && (skillCon.effectTime == 0) && (skillGauge.color != AppConst.CAN_USE_SKILL_GAUGE_COLOR))
        {
            skillGauge.color = AppConst.CAN_USE_SKILL_GAUGE_COLOR;
        }

        // スキルが使用不可かつ白色ではないなら白色にする
        else if (((skillCon.coolDown != 0) || (skillCon.effectTime != 0)) && (skillGauge.color != AppConst.DEFAULT_SKILL_GAUGE_COLOR))
        {
            skillGauge.color = AppConst.DEFAULT_SKILL_GAUGE_COLOR;
        }
    }

    // スキルのUIを初期化
    void Init()
    {
        skillGauge.fillAmount = 1;
    }

    void Start()
    {
        init = Initialize.instance;
        skillCon = SkillController.instance;
        localize = Localize.instance;

        // フォントを設定
        skillNameText.font = localize.GetFont();

        // デリゲートに初期化関数を登録
        init.init_Stage += Init;
    }

    private void Update()
    {
        DrawSkillUI();
    }

    private void OnDestroy()
    {
        init.init_Stage -= Init;
    }
}
