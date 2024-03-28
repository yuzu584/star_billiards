using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 設定画面のUIを管理
public class OptionsUIController : MonoBehaviour
{
    public GameObject[] lootObj;    // 階層ごとのゲームオブジェクト

    private OptionsController opCon;

    private void Start()
    {
        opCon.opUICon = this;
    }

    private void OnDestroy()
    {
        opCon.opUICon = null;
    }

    // 表示する階層を切り替え
    private void SwitchLoot()
    {
        // 階層ごとのオブジェクトの数繰り返す
        for (int i = 0; i < lootObj.Length; ++i)
        {
            // 表示する階層なら表示
            if (i == (int)opCon.loot)
                lootObj[i].SetActive(true);
            // 非表示
            else
                lootObj[i].SetActive(false);
        }
    }

    void OnEnable()
    {
        opCon = OptionsController.instance;

        // 階層を一番上に
        opCon.loot = OptionsController.Loot.Top;

        // 最初は Top を表示
        SwitchLoot();
    }

    void Update()
    {
        // 階層が変わったら画面を切り替える
        if (opCon.oldLoot != opCon.loot)
        {
            opCon.oldLoot = opCon.loot;
            SwitchLoot();
        }
    }
}
