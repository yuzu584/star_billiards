using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System;

using Const;

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
        skillName = localize.GetString_SkillName((EnumSkillName)Enum.ToObject(typeof(EnumSkillName),skillCon.selectSkill));

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
