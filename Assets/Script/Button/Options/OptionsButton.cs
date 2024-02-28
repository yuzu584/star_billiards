using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

// 設定画面のボタン
public class OptionsButton : Button
{
    [SerializeField] private OptionsController _optionsController;
    [SerializeField] private int num = 0; // 遷移先の設定項目の番号

    // マウスポインターが乗った時の処理
    protected override void EnterProcess()
    {
        // ボタンのアニメーション処理
        BtnAnimProcess(imageStructs, textStructs, true);
    }

    // マウスポインターが離れたときの処理
    protected override void ExitProcess()
    {
        // ボタンのアニメーション処理
        BtnAnimProcess(imageStructs, textStructs, false);
    }

    // クリックされたときの処理
    protected override void ClickProcess()
    {
        // 設定画面の階層を変更
        _optionsController.loot = (OptionsController.Loot)Enum.ToObject(typeof(OptionsController.Loot), num);
    }

    void OnEnable()
    {
        // ボタンの初期化処理
        BtnInit(imageStructs, textStructs);
    }
}
