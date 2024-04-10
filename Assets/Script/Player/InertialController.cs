using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AppConst;

// プレイヤーの慣性を制御
public class InertialController : MonoBehaviour
{
    private InputController input;

    public float EaseOfBending = 1.0f; // 軌道の曲げやすさ
    Rigidbody rb;                      // プレイヤーのRigidbody

    void Start()
    {
        // rigidbodyを取得
        rb = GetComponent<Rigidbody>();

        input = InputController.instance;

        // ゲーム中に移動したときに発火するイベントに登録
        input.game_OnMoveDele += Process;
    }

    // プレイヤーの慣性を制御する処理
    void Process(Vector2 mVec)
    {
        // 前入力なら減速を緩やかに
        if (mVec.y > 0)
        {
            rb.velocity *= Const_Player.SPEED_MAINTENANCE_RATE;
        }
        // 後ろ入力なら減速
        else if (mVec.y < 0)
        {
            rb.velocity *= Const_Player.SPEED_REDUCTION_RATE;
        }
        // 前後入力されていない状態で左右入力なら軌道を左右に曲げる
        else if (mVec.x != 0)
        {
            rb.AddForce(Camera.main.transform.right * (rb.velocity.magnitude / 10) * mVec.x * EaseOfBending);
        }

        // 速度が一定の値以下なら0にする
        if (rb.velocity.magnitude < Const_Player.SPEED_THRESHOLD)
        {
            rb.velocity *= 0;
        }
    }
}
