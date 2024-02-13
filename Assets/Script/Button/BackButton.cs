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
        // ボタンのアニメーション
        StopAllCoroutines();
        for(int i = 0; i < imageStructs.Length;  i++)
            StartCoroutine(lerp.Color_Image(imageStructs[i].image, imageStructs[i].startColor, imageStructs[i].endColor, imageStructs[i].fadeTime));
    }

    // マウスポインターが離れたときの処理
    protected override void ExitProcess()
    {
        // ボタンのアニメーション
        StopAllCoroutines();
        for (int i = 0; i < imageStructs.Length; i++)
            StartCoroutine(lerp.Color_Image(imageStructs[i].image, imageStructs[i].endColor, imageStructs[i].startColor, imageStructs[i].fadeTime));
    }

    // クリックされたときの処理
    protected override void ClickProcess()
    {
        // 画面番号を前の画面にする
        screenController.screenNum = oldScreen;
    }

    // 前回のスクリーン番号をセット
    void SetOldScreen()
    {
        oldScreen = screenController.oldScreenNum;
    }

    void OnEnable()
    {
        // ボタンの色をリセット
        StopAllCoroutines();
        for (int i = 0; i < imageStructs.Length; i++)
            imageStructs[i].image.color = imageStructs[i].startColor;
    }

    new void Start()
    {
        base.Start();

        // デリゲートを追加
        screenController.changeScreen += SetOldScreen;
    }
}
