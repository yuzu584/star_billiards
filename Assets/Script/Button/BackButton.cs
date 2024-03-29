using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEditor;

// 戻るボタンを管理
public class BackButton : Button
{
    private ScreenController.ScreenType oldScreen = 0; // 前回のスクリーン(戻り先の画面)

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
        // 画面を前の画面にする
        scrCon.ScreenLoot = 0;
        scrCon.Screen = oldScreen;
    }

    // 前回のスクリーン番号をセット
    void SetOldScreen()
    {
        oldScreen = scrCon.oldScreen;
    }

    // 画面を戻る
    void Back(float v)
    {
        // 階層が0以下かつオブジェクトが有効なら
        if ((scrCon.ScreenLoot <= 0) && (gameObject.activeInHierarchy))
        {
            //音を再生
            if (sound != null)
                StartCoroutine(sound.Play(ClickSound));

            // 前の画面に戻る
            scrCon.Screen = oldScreen;
        }
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
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        scrCon.changeScreen -= SetOldScreen;
        input.ui_OnNegativeDele -= Back;
    }
}
