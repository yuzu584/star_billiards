using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// マウスカーソルを管理
public class CursorController : MonoBehaviour
{
    void Start()
    {
        // マウスカーソルを非表示
        DrawCursol(false);
    }

    // マウスカーソルを表示又は非表示にする
    public void DrawCursol(bool draw)
    {
        // カーソルの表示非表示切り替え
        Cursor.visible = draw;

        // カーソルを表示するなら
        if(draw)
        {
            // カーソルの固定を解除
            Cursor.lockState = CursorLockMode.None;
        }
        // カーソルが非表示なら
        else
        {
            // カーソルを画面中央に固定
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
