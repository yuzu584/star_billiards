using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// マウスカーソルを管理
public class CursorController : MonoBehaviour
{
    void Start()
    {
        // カーソルを画面中央に固定
        Cursor.lockState = CursorLockMode.Locked;

        // カーソル非表示
        Cursor.visible = false;
    }
}
