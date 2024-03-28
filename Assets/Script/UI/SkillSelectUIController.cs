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

    void Start()
    {
        skillSelect = SkillSelect.instance;

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
    void DrawSkillInfo(int skillNum)
    {
        skillName.text = AppConst.SKILL_NAME[skillNum];
        cost.text = AppConst.SKILL_ENERGY_USAGE[skillNum].ToString("0");
        effectTime.text = AppConst.SKILL_EFFECT_TIME[skillNum].ToString("0") + "s";
        coolDown.text = AppConst.SKILL_COOLDOWN[skillNum].ToString("0") + "s";
        effectDetails.text = AppConst.SKILL_DETAILS[skillNum];
    }
}