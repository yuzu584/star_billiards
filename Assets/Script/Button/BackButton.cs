using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEditor;

// 戻るボタンを管理
public class BackButton : Button
{
    [System.Serializable]
    private struct ImageStruct // 画像の構造体
    {
        public Image image;      // 画像
        public Color startColor; // 変化前の色
        public Color endColor;   // 変化後の色
        public float fadeTime;   // フェード時間
    }
    [SerializeField] private ImageStruct[] imageStructs;

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

        // ボタンの色をリセット
        for (int i = 0; i < imageStructs.Length; i++)
            imageStructs[i].image.color = imageStructs[i].startColor;
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
