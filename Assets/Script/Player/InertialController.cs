using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// プレイヤーの慣性を制御
public class InertialController : MonoBehaviour
{
    public float EaseOfBending = 1.0f; // 軌道の曲げやすさ
    Rigidbody rb;                      // プレイヤーのRigidbody
    float x = 0;                       // 左右移動量
    float z = 0;                       // 前後移動量
    Vector3 vector;                    // 移動方向

    void Start()
    {
        // rigidbodyを取得
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 左右移動量を代入
        x = Input.GetAxisRaw("Horizontal");

        // 前後移動量を代入
        z = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // 前入力なら減速を緩やかに
        if (z > 0)
        {
            rb.velocity *= AppConst.SPEED_MAINTENANCE_RATE;
        }
        // 後ろ入力なら減速
        else if (z < 0)
        {
            rb.velocity *= AppConst.SPEED_REDUCTION_RATE;
        }
        // 前後入力されていない状態で左右入力なら軌道を左右に曲げる
        else if (x != 0)
        {
            rb.AddForce(Camera.main.transform.right * (rb.velocity.magnitude / 10) * x * EaseOfBending);
        }

        // 速度が一定の値以下なら0にする
        if (rb.velocity.magnitude < AppConst.SPEED_THRESHOLD)
        {
            rb.velocity *= 0;
        }
    }
}
