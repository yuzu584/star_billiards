using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// カメラの視点移動
public class TPSCamera : MonoBehaviour
{
    [SerializeField] private InputController input; // InspectorでInputControllerを指定
    [SerializeField] private GameObject player;     // プレイヤー
    [SerializeField] private float speed = 1.0f;    // 視点移動速度

    void Start()
    {
        input.game_OnLookDele += MoveCameraAngle;
    }

    // 視点移動処理
    void MoveCameraAngle(Vector2 vec)
    {
        // X方向に一定量移動していれば横回転
        if (Mathf.Abs(vec.x) > 0.001f)
        {
            // 回転軸はワールド座標のY軸
            transform.RotateAround(player.transform.position, transform.up, vec.x * speed);
        }

        // Y方向に一定量移動していれば縦回転
        if (Mathf.Abs(vec.y) > 0.001f)
        {
            // 回転軸はカメラ自身のX軸
            transform.RotateAround(player.transform.position, transform.right, -vec.y * speed);
        }
    }
}
