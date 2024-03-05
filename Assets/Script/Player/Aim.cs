using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// エイムする
public class Aim : MonoBehaviour
{
    [SerializeField] private ScreenController screenController; // InspectorでScreenControllerを指定
    [SerializeField] private InputController input;             // InspectorでInputControllerを指定

    private float inputValue; // 発射ボタン2の入力

    void Update()
    {
        // 発射ボタン2の入力を取得
        inputValue = input.Game_Aim;

        // ゲーム画面なら
        if (screenController.ScreenNum == 5)
        {
            // 発射ボタン2が押されたら
            if (inputValue > 0)
            {
                // 時間の流れをスローにする
                Time.timeScale = AppConst.SLOW_TIME_SCALE;
            }
            // 発射ボタン2が押されてないなら
            else if (inputValue == 0)
            {
                // 時間の流れを元に戻す
                Time.timeScale = AppConst.DEFAULT_TIME_SCALE;
            }
        }
    }
}
