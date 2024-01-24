using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// スキル選択画面のUIを管理
public class SkillSelectUIController : MonoBehaviour
{
    [SerializeField] private GameObject skillSlotObj; // スキルスロットのプレハブ
    [SerializeField] private GameObject parentObj;    // スキルスロットのプレハブの親オブジェクト

    void Start()
    {
        // スキル選択画面のスキルスロットを作成
        CreateSkillSlot();
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
}