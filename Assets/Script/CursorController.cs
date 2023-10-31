using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// マウスカーソルを管理
public class CursorController : MonoBehaviour
{
    [SerializeField] ScreenController screenController; // ScreenController型の変数

    void Start()
    {
        // カーソルを画面中央に固定
        Cursor.lockState = CursorLockMode.Locked;

        // カーソル非表示
        Cursor.visible = false;
    }

    void Update()
    {
        // 戻るボタンが押されたら
        if(Input.GetButtonDown("Cancel"))
        {
            // 画面番号によって分岐
            switch (screenController.screenNum)
            {
                case 0: // InGame

                    // カーソルを画面中央に固定
                    Cursor.lockState = CursorLockMode.Locked;

                    // カーソル非表示
                    Cursor.visible = false;
                    break;

                case 1: // Pause

                    // カーソルの固定を解除
                    Cursor.lockState = CursorLockMode.None;

                    // カーソル表示
                    Cursor.visible = true;
                    break;
            }
        }
    }
}
