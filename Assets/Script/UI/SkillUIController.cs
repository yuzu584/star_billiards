using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// スキルのUIを管理
public class SkillUIController : MonoBehaviour
{
    [SerializeField] private UIController uIController; // InspectorでUIControllerを指定

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
    }
}
