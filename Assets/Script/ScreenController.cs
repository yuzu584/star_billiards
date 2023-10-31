using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 画面の種類を管理
public class ScreenController : MonoBehaviour
{
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
        }
        // ポーズ中に戻るボタンが押されたら
        else if (Input.GetButtonDown("Cancel") && screenNum == 1)
        {
            // ゲーム画面に遷移
            screenNum = 0;
        }
    }
}
