using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// プレイヤーを管理
public class PlayerController : Singleton<PlayerController>
{
    public Rigidbody rb;          // プレイヤーのリジットボディ

    private Initialize init;

    // 移動速度を描画するUIのデリゲート
    public delegate void SpeedUIDele();
    public SpeedUIDele speedUIDele;

    // プレイヤーに関する数値を初期化
    void Init()
    {
        rb.velocity *= 0;
        transform.position = Const_Player.PLATER_DEFAULT_POSITION;

        // 移動制限を解除
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Start()
    {
        init = Initialize.instance;

        // デリゲートに初期化関数を登録
        init.init_Stage += Init;
    }

    private void Update()
    {
        speedUIDele?.Invoke();
    }
}
