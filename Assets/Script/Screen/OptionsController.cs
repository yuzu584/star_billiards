using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

// 設定画面を管理
public class OptionsController : MonoBehaviour
{
    public enum Loot   // 階層
    {
        Top = 0,       // 最初の画面
        GamePlay = 1,  // ゲーム内設定
        Video = 2,     // ビデオ設定
        Audio = 3,     // オーディオ設定
        KeyConfig = 4, // キー配置設定
        Language = 5,  // 言語設定
    }
    public Loot loot = 0;

    private int oldLoot = 0;  // 1フレーム前の階層

    [SerializeField] private OptionsUIController _optionsUIController;
    [SerializeField] private FOV fov;
    [SerializeField] private GameObject[] lootObj; // 階層ごとのゲームオブジェクト


    // 表示する階層を切り替え
    private void SwitchLoot()
    {
        for(int i = 0; i < lootObj.Length; ++i)
        {
            if(i == (int)loot)
                lootObj[i].SetActive(true);
            else
                lootObj[i].SetActive(false);
        }
    }

    void OnEnable()
    {
        loot = Loot.Top;
        fov.ResetFOV();
    }

    void Update()
    {
        // 階層が変わったら画面を切り替える
        if(oldLoot != (int)loot)
        {
            oldLoot = (int)loot;
            SwitchLoot();
        }
    }
}
