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
    public void DrawSpeedValue(Text speedValue)
    {
        // ポーズ画面かつUIが表示されているなら非表示にする
        if ((screenController.screenNum == 1) && (speedValue.enabled == true))
        {
            speedValue.enabled = false;
        }
        // ゲーム画面なら
        else if (screenController.screenNum == 0)
        {
            // 非表示なら表示
            if (speedValue.enabled == false)
            {
                speedValue.enabled = true;
            }

            // 速度のテキストを更新
            speedValue.text = rb.velocity.magnitude.ToString("0") + " km/s";
        }
    }
}
