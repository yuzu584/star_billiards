using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// スキル選択画面のUIを管理
public class SkillSelectUIController : MonoBehaviour
{
    [SerializeField] private UIController uIController; // InspectorでUIControllerを指定
    [SerializeField] private GameObject skillSlotObj;   // スキルスロットのプレハブ
    [SerializeField] private GameObject parentObj;      // スキルスロットのプレハブの親オブジェクト

    void Start()
    {
        // スキル選択画面のスキルスロットを作成
        CreateSkillSlot();

        // スキルの情報を描画
        DrawSkillInfo(0);
    }

    // スキル選択画面のスキルスロットを作成
    void CreateSkillSlot()
    {
        // スキルの数繰り返す
        for (int i = 0; i < AppConst.SKILL_NUM; i++)
        {
            // インスタンスを生成
            GameObject slotPrefab = Instantiate(skillSlotObj);

            // 名前を設定
            slotPrefab.name = AppConst.SKILL_NAME[i];

            // 位置を設定
            slotPrefab.transform.position = new Vector3(-100.0f + (i * 60), 100.0f - ((i / 5) * 60), 0.0f);

            // 親を設定
            slotPrefab.transform.SetParent(parentObj.transform, false);

            // スキル番号を設定
            SkillSlotController skillSlotController = slotPrefab.GetComponent<SkillSlotController>();
            skillSlotController.skillNum = i;
        }
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