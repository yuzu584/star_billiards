using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 戻るボタンを管理
public class BackButton : Button
{
    public ScreenController.ScreenType oldScreen = 0; // 前回のスクリーン(戻り先の画面)

    public Action action;

    // マウスポインターが乗った時の処理
    public override void EnterProcess()
    {
        // ボタンのアニメーション処理
        BtnAnimProcess(imageStructs, textStructs, true);
    }

    // マウスポインターが離れたときの処理
    public override void ExitProcess()
    {
        // ボタンのアニメーション処理
        BtnAnimProcess(imageStructs, textStructs, false);
    }

    // クリックされたときの処理
    public override void ClickProcess()
    {
        action?.Invoke();
    }

    // 前回のスクリーン番号をセット
    void SetOldScreen()
    {
        oldScreen = scrCon.oldScreen;
    }

    // 画面を戻る
    void Back(float v)
    {
        action?.Invoke();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();

        // デリゲートを追加
        scrCon.changeScreen += SetOldScreen;
        input.ui_OnNegativeDele += Back;

        SetOldScreen();

        action = () =>
        {
            // 階層が0以下かつオブジェクトが有効なら
            if ((scrCon.ScreenLoot <= 0) && (gameObject.activeInHierarchy))
            {
                //音を再生
                if (sound != null)
                    PlayBtnSound(BtnSounds.ClickSound);

                // 前の画面に戻る
                scrCon.Screen = oldScreen;
            }
        };
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        scrCon.changeScreen -= SetOldScreen;
        input.ui_OnNegativeDele -= Back;
    }
}
