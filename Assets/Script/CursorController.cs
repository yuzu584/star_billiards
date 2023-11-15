using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// マウスカーソルを管理
public class CursorController : MonoBehaviour
{
    [SerializeField] private ScreenData screenData;             // InspectorでScreenDataを指定
    [SerializeField] private ScreenController screenController; // InspectorでScreenControllerを指定

    private bool draw; // カーソルを表示するかどうか

    void Update()
    {
        // 現在のスクリーン番号でカーソルを表示するかどうか決める
        draw = screenData.screenList[screenController.screenNum].drawCursol;

        // カーソルの表示非表示切り替え
        Cursor.visible = draw;

        // カーソルを表示するなら
        if (draw)
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
