using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// エイムする
public class Aim : MonoBehaviour
{
    [SerializeField] private ScreenController screenController; // InspectorでScreenControllerを指定
    [SerializeField] private InputController input;             // InspectorでInputControllerを指定

    void Start()
    {
        input.game_OnAimDele += Process;
    }

    // エイム時の処理
    void Process(float value)
    {
        // エイムボタンが押されたら
        if (value > 0)
        {
            // 時間の流れをスローにする
            Time.timeScale = AppConst.SLOW_TIME_SCALE;
        }
        // エイムボタンが押されてないなら
        else if (value == 0)
        {
            // 時間の流れを元に戻す
            Time.timeScale = AppConst.DEFAULT_TIME_SCALE;
        }
    }
}
