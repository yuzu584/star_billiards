using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

// プレイヤーを管理
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;          // プレイヤーのリジットボディ

    private Initialize init;

    // プレイヤーに関する数値を初期化
    void Init()
    {
        rb.velocity *= 0;
        transform.position = AppConst.PLATER_DEFAULT_POSITION;
    }

    void Start()
    {
        init = Initialize.instance;

        // デリゲートに初期化関数を登録
        init.init_Stage += Init;
    }
}
