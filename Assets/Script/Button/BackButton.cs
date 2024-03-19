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
        // 画面番号を前の画面にする
        scrCon.ScreenLoot = 0;
        scrCon.ScreenNum = oldScreen;
    }

    // 前回のスクリーン番号をセット
    void SetOldScreen()
    {
        oldScreen = scrCon.oldScreenNum;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // ボタンの初期化処理
        BtnInit(imageStructs, textStructs);
    }

    protected override void Start()
    {
        base.Start();

        // デリゲートを追加
        scrCon.changeScreen += SetOldScreen;
        input.ui_OnNegativeDele += (float value) =>
        {
            // 階層が0以下かつオブジェクトが有効なら
            if ((scrCon.ScreenLoot <= 0) && (this.gameObject.activeInHierarchy))
            {
                //音を再生
                if (sound != null)
                    StartCoroutine(sound.Play(ClickSound));

                // 前の画面に戻る
                scrCon.ScreenNum = oldScreen;
            }

        };

        SetOldScreen();
    }
}
