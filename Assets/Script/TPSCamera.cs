using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// カメラの視点移動
public class TPSCamera : MonoBehaviour
{
    // Inspectorでプレイヤーを指定
    [SerializeField] GameObject player;

    public float speed = 1.0f;  // 視点移動速度
    float mx;                   // マウスの横移動量
    float my;                   // マウスの縦移動量

    void Update()
    {
        // マウスの移動量を取得
        mx = Input.GetAxis("Mouse X");
        my = Input.GetAxis("Mouse Y");

        // X方向に一定量移動していれば横回転
        if (Mathf.Abs(mx) > 0.001f)
        {
            // 回転軸はワールド座標のY軸
            transform.RotateAround(player.transform.position, transform.up, mx * speed);
        }

        // Y方向に一定量移動していれば縦回転
        if (Mathf.Abs(my) > 0.001f)
        {
            // 回転軸はカメラ自身のX軸
            transform.RotateAround(player.transform.position, transform.right, -my * speed);
        }
    }
}
