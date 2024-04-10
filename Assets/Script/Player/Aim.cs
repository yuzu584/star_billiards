using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// エイムする
public class Aim : MonoBehaviour
{
    private InputController input;

    void Start()
    {
        input = InputController.instance;

        input.game_OnAimDele += Process;
    }

    // エイム時の処理
    void Process(float value)
    {
        // エイムボタンが押されたら
        if (value > 0)
        {
            // 時間の流れをスローにする
            Time.timeScale = Const_System.SLOW_TIME_SCALE;
        }
        // エイムボタンが押されてないなら
        else if (value == 0)
        {
            // 時間の流れを元に戻す
            Time.timeScale = Const_System.DEFAULT_TIME_SCALE;
        }
    }
}
