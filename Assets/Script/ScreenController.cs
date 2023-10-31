using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 画面の種類を管理
public class ScreenController : MonoBehaviour
{
    [SerializeField] private CursorController cursorController; // CursorController型の変数
    [SerializeField] private UIController uIController;         // UIController型の変数

    // 画面番号
    // InGame = 0
    // Pause  = 1
    public int screenNum = 0;

    void Update()
    {
        // ゲーム中に戻るボタンが押されたら
        if(Input.GetButtonDown("Cancel") && screenNum == 0)
        {
            // ポーズ画面に遷移
            screenNum = 1;

            // マウスカーソルを表示
            cursorController.DrawCursol(true);

            // ポーズ画面のUIを表示
            uIController.DrawPauseUI(true);

            // 時間の流れを止める
            Time.timeScale = 0.0f;
        }
    }
}
