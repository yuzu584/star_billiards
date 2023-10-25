using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    void Update()
    {
        // 発射ボタン2が押されたら
        if (Input.GetAxisRaw("Fire2") > 0)
        {
            // 時間の流れをスローにする
            Time.timeScale = 0.1f;
        }
        // 発射ボタン2が押されてないなら
        else if (Input.GetAxisRaw("Fire2") == 0)
        {
            // 時間の流れを元に戻す
            Time.timeScale = 1f;
        }
    }
}
