using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// タイトル画面を管理
public class TitleController : MonoBehaviour
{
    private InputController input;
    private KeyGuideUI keyGuideUI;

    private static bool isFirstDraw = true;

    void Start()
    {
        input = InputController.instance;
        keyGuideUI = KeyGuideUI.instance;

        // 最初のタイトル描画時のみ入力の設定を行う
        if (isFirstDraw)
        {
            isFirstDraw = false;
            input.SetInputs();
        }

        // キー操作ガイドを削除
        keyGuideUI.DestroyGuide();
    }
}
