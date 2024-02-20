using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// InputSystemの入力を管理
public class InputController : MonoBehaviour
{
    private PlayerActions _inputs; // InputActionAssetのラッパークラス

    private void Awake()
    {
        // InputActionAssetのラッパークラスをインスタンス化
        _inputs = new PlayerActions();

        // 入力を有効化
        SetInputs(true);
    }

    // 入力状態の有効無効を設定
    public void SetInputs(bool canInput)
    {
        // 入力を有効化
        if(canInput)
        {
            _inputs.Enable();
        }
        // 入力を無効化
        else
        {
            _inputs.Disable();
        }
    }
}
