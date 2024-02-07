using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// エイムする
public class Aim : MonoBehaviour
{
    [SerializeField] private ScreenController screenController; // InspectorでScreenControllerを指定

    void Update()
    {
        // ゲーム画面なら
        if (screenController.screenNum == 5)
        {
            // 発射ボタン2が押されたら
            if (Input.GetAxisRaw("Fire2") > 0)
            {
                // 時間の流れをスローにする
                Time.timeScale = AppConst.SLOW_TIME_SCALE;
            }
            // 発射ボタン2が押されてないなら
            else if (Input.GetAxisRaw("Fire2") == 0)
            {
                // 時間の流れを元に戻す
                Time.timeScale = AppConst.DEFAULT_TIME_SCALE;
            }
        }
    }
}
