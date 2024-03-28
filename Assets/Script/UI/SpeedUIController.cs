using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// 移動速度のUIを管理
public class SpeedUIController : MonoBehaviour
{
    [SerializeField] private Text speedText;                    // 速度を表すテキスト

    private Rigidbody rb;                                       // プレイヤーのRigidbody

    private PlayerController playerCon;

    // 移動速度の数値を描画
    void Draw()
    {
        // 速度のテキストを更新
        speedText.text = rb.velocity.magnitude.ToString("0") + " km/s";
    }

    private void Start()
    {
        playerCon = PlayerController.instance;

        rb = playerCon.rb;

        // 移動速度のUIを描画するデリゲートに登録
        playerCon.speedUIDele += Draw;
    }

    private void OnDestroy()
    {
        playerCon.speedUIDele -= Draw;
    }
}
