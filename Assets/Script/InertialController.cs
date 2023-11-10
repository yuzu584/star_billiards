using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // 前入力なら加速
        if (z > 0)
        {
            rb.velocity *= 1.05f;
        }
        // 後ろ入力なら減速
        else if (z < 0)
        {
            rb.velocity *= 0.95f;
        }
    }
}
