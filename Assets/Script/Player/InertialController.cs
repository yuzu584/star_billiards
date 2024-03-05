using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// プレイヤーの慣性を制御
public class InertialController : MonoBehaviour
{
    [SerializeField] private InputController input; // InspectorでInputControllerを指定

    public float EaseOfBending = 1.0f; // 軌道の曲げやすさ
    Rigidbody rb;                      // プレイヤーのRigidbody
    Vector2 mVec;                      // 前後左右の移動ベクトル
    Vector3 vector;                    // 移動方向

    void Start()
    {
        // rigidbodyを取得
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 移動量を代入
        mVec = input.Game_Move;
    }

    void FixedUpdate()
    {
        // 前入力なら減速を緩やかに
        if (mVec.y > 0)
        {
            rb.velocity *= AppConst.SPEED_MAINTENANCE_RATE;
        }
        // 後ろ入力なら減速
        else if (mVec.y < 0)
        {
            rb.velocity *= AppConst.SPEED_REDUCTION_RATE;
        }
        // 前後入力されていない状態で左右入力なら軌道を左右に曲げる
        else if (mVec.x != 0)
        {
            rb.AddForce(Camera.main.transform.right * (rb.velocity.magnitude / 10) * mVec.x * EaseOfBending);
        }

        // 速度が一定の値以下なら0にする
        if (rb.velocity.magnitude < AppConst.SPEED_THRESHOLD)
        {
            rb.velocity *= 0;
        }
    }
}
