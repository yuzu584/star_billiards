using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 設定画面のUIを管理
public class OptionsUIController : MonoBehaviour
{
    private OptionsController optCon;

    private void Start()
    {
        optCon ??= OptionsController.instance;

        SwitchLoot();
    }

    // 表示する階層を切り替え
    private void SwitchLoot()
    {
        for (int i = 0; i < optCon.lootObj.Length; ++i)
        {
            if (i == (int)optCon.loot)
                optCon.lootObj[i].SetActive(true);
            else
                optCon.lootObj[i].SetActive(false);
        }
    }

    void OnEnable()
    {
        optCon ??= OptionsController.instance;

        // 階層を一番上に
        optCon.loot = OptionsController.Loot.Top;
    }

    void Update()
    {
        // 階層が変わったら画面を切り替える
        if (optCon.oldLoot != (int)optCon.loot)
        {
            optCon.oldLoot = (int)optCon.loot;
            SwitchLoot();
        }
    }
}
