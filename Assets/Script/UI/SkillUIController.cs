using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Const;

// スキルのUIを管理
public class SkillUIController : MonoBehaviour
{
    [SerializeField] private UIController uIController; // InspectorでUIControllerを指定
    [SerializeField] private Initialize initialize;     // InspectorでInitializeを指定

    // スキルのUIを描画
    public void DrawSkillUI(string skillName, float coolDown, float effectTime, float nowCoolDown, float nowEffectTime)
    {
        // テキストを現在のスキル名に変更
        uIController.skillUI.skillName.text = skillName;

        // 効果時間を描画
        if (nowEffectTime > 0)
            uIController.skillUI.skillGauge.fillAmount = nowEffectTime / effectTime;

        // 効果時間が経過していたならクールダウンを描画
        else if (nowCoolDown > 0)
            uIController.skillUI.skillGauge.fillAmount = (coolDown - nowCoolDown) / coolDown;

        // スキルが使用可能かつ黄色ではないなら黄色にする
        if((nowCoolDown == 0) && (nowEffectTime == 0) && (uIController.skillUI.skillGauge.color != AppConst.CAN_USE_SKILL_GAUGE_COLOR))
        {
            uIController.skillUI.skillGauge.color = AppConst.CAN_USE_SKILL_GAUGE_COLOR;
        }

        // スキルが使用不可かつ白色ではないなら白色にする
        else if (((nowCoolDown != 0) || (nowEffectTime != 0)) && (uIController.skillUI.skillGauge.color != AppConst.DEFAULT_SKILL_GAUGE_COLOR))
        {
            uIController.skillUI.skillGauge.color = AppConst.DEFAULT_SKILL_GAUGE_COLOR;
        }
    }

    // スキルのUIを初期化
    void Init()
    {
        uIController.skillUI.skillGauge.fillAmount = 1;
    }

    void Start()
    {
        // デリゲートに初期化関数を登録
        initialize.init_Stage += Init;
    }
}
