using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEditor;

// 戻るボタンを管理
public class BackButton : Button
{
    [SerializeField] private Image image; // 画像

    private int oldScreen = 0;                                    // 前回のスクリーン(戻り先の画面)
    private Color startColor = new Color(1.0f, 1.0f, 1.0f, 0.0f); // 変化前の色
    private Color endColor = new Color (1.0f, 1.0f, 1.0f, 0.1f);  // 変化後の色
    private Color nowColor;                                       // 現在の色
    private float fadeTime = 0.1f;                                // フェード時間

    // マウスポインターが乗った時の処理
    protected override void EnterProcess()
    {
        // ボタンのアニメーション
        nowColor = image.color;

        StopAllCoroutines();
        StartCoroutine(lerp.Color_Image(image, nowColor, endColor, fadeTime));
    }

    // マウスポインターが離れたときの処理
    protected override void ExitProcess()
    {
        // ボタンのアニメーション
        nowColor = image.color;

        StopAllCoroutines();
        StartCoroutine(lerp.Color_Image(image, nowColor, startColor, fadeTime));
    }

    // クリックされたときの処理
    protected override void ClickProcess()
    {
        // 画面番号を前の画面にする
        screenController.screenNum = oldScreen;

        // ボタンの色をリセット
        image.color = startColor;
    }

    // 前回のスクリーン番号をセット
    void SetOldScreen()
    {
        oldScreen = screenController.oldScreenNum;
    }

    new void Start()
    {
        base.Start();

        // デリゲートを追加
        screenController.changeScreen += SetOldScreen;
    }
}
