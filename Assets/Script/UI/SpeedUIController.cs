using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// 移動速度のUIを管理
public class SpeedUIController : Singleton<SpeedUIController>
{
    [SerializeField] private ScreenController screenController; // InspectorでScreenControllerを指定
    [SerializeField] private Rigidbody rb;                      // プレイヤーのRigidbody

    // 移動速度の数値を描画
    public void DrawSpeedValue(Text speedValue)
    {
        // 速度のテキストを更新
        speedValue.text = rb.velocity.magnitude.ToString("0") + " km/s";
    }
}
