using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// マウスカーソルを管理
public class CursorController : MonoBehaviour
{
    [SerializeField] private ScreenData scrData;             // InspectorでScreenDataを指定

    private ScreenController scrCon;
    private bool draw;                                       // カーソルを表示するかどうか

    private void Start()
    {
        scrCon = ScreenController.instance;

        scrCon.changeScreen += SwitchCursorState;
    }

    // カーソルの状態を切り替え
    void SwitchCursorState()
    {
        // 現在のスクリーン番号でカーソルを表示するかどうか決める
        draw = scrData.screenList[(int)scrCon.Screen].drawCursol;

        // カーソルの表示非表示切り替え
        Cursor.visible = draw;

        // カーソルを表示するなら固定を解除
        if (draw)
            Cursor.lockState = CursorLockMode.None;

        // カーソルが非表示なら画面中央に固定
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
}
