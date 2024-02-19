using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// スキル選択画面のUIを管理
public class SkillSelectUIController : MonoBehaviour
{
    [SerializeField] private UIController uIController; // InspectorでUIControllerを指定

    void Start()
    {
        // スキルの情報を描画
        DrawSkillInfo(0);
    }

    // スキルの情報を描画
    public void DrawSkillInfo(int skillNum)
    {
        uIController.skillSelectUI.name.text = AppConst.SKILL_NAME[skillNum];
        uIController.skillSelectUI.cost.text = AppConst.SKILL_ENERGY_USAGE[skillNum].ToString("0");
        uIController.skillSelectUI.effectTime.text = AppConst.SKILL_EFFECT_TIME[skillNum].ToString("0") + "s";
        uIController.skillSelectUI.coolDown.text = AppConst.SKILL_COOLDOWN[skillNum].ToString("0") + "s";
        uIController.skillSelectUI.effectDetails.text = AppConst.SKILL_DETAILS[skillNum];
    }
}