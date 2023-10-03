using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    // カメラ移動カウント
    public static float cameraMoveCount = 0;

    void Update()
    {
        // 発射ボタン2が押されたら
        if (Input.GetAxisRaw("Fire2") > 0)
        {
            // 時間の流れをスローにする
            Time.timeScale = 0.1f;

            // カメラ移動カウントを増加
            cameraMoveCount += 0.1f;
        }
        // 発射ボタン2が押されてないなら
        else if (Input.GetAxisRaw("Fire2") == 0)
        {
            // 時間の流れを元に戻す
            Time.timeScale = 1f;

            // カメラ移動カウントを減少
            cameraMoveCount -= 0.1f;
        }

        if (cameraMoveCount < 0)
        {
            cameraMoveCount = 0;
        }
        else if(cameraMoveCount > 1)
        {
            cameraMoveCount = 1;
        }
    }
}
