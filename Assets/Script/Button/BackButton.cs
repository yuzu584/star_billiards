using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEditor;

// 戻るボタンを管理
public class BackButton : Button
{
    private int oldScreen = 0; // 前回のスクリーン(戻り先の画面)

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
        // 画面番号を前の画面にする
        screenController.ScreenNum = oldScreen;
    }

    // 前回のスクリーン番号をセット
    void SetOldScreen()
    {
        oldScreen = screenController.oldScreenNum;
    }

    new void OnEnable()
    {
        base.OnEnable();

        // ボタンの初期化処理
        BtnInit(imageStructs, textStructs);
    }

    new void Start()
    {
        base.Start();

        // デリゲートを追加
        screenController.changeScreen += SetOldScreen;
        input.ui_OnNegativeDele += (float value) =>
        {
            if (screenController.ScreenLoot <= 0)
            {
                if(screenController.ScreenNum == 1)
                    screenController.ScreenNum = 0;
                else
                    screenController.ScreenNum = oldScreen;
            }

        };

        SetOldScreen();
    }
}
