using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// 移動速度のUIを管理
public class SpeedUIController : MonoBehaviour
{
    [SerializeField] private ScreenController screenController; // InspectorでScreenControllerを指定
    [SerializeField] private Rigidbody rb;                      // プレイヤーのRigidbody

    // 移動速度の数値を描画
    public void DrawSpeedValue(bool draw, Text speedValue)
    {
        // 描画するなら
        if(draw)
        {
            // 速度のテキストを更新
            speedValue.text = rb.velocity.magnitude.ToString("0") + " km/s";
        }

        // 表示/非表示切り替え
        if(speedValue.enabled != draw)
        {
            speedValue.enabled = draw;
        }
    }
}
