using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEditor;

// 戻るボタンを管理
public class BackButton : Button
{
    [SerializeField] private Image backGround;          // 背景画像
    [SerializeField] private Text text;                 // テキスト

    private int oldScreen = 1;                          // 前回のスクリーン(戻り先の画面)
    private Color startColor = new Color(0, 0, 0, 0);   // 変化前の色
    private Color endColor = new Color (0, 0, 0, 0.1f); // 変化後の色

    // マウスポインターが乗った時の処理
    public override void EnterProcess()
    {
        // ボタンのアニメーション
        StopAllCoroutines();
        lerp.Color_Image(backGround, startColor, endColor, 0.2f);
    }

    // マウスポインターが離れたときの処理
    public override void ExitProcess()
    {
        // ボタンのアニメーション
        StopAllCoroutines();
        lerp.Color_Image(backGround, endColor, startColor, 0.2f);
    }

    // クリックされたときの処理
    public override void ClickProcess()
    {
        // 画面番号を前の画面にする
        screenController.screenNum = oldScreen;
    }
}
