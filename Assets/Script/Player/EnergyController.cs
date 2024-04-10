using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AppParam;

// エネルギーの増減を管理
public class EnergyController : Singleton<EnergyController>
{
    private ScreenController screenCon;
    private Initialize init;
    private StageController stageCon;

    private Rigidbody rb;           // リジッドボディ

    // 初期化処理
    void Init()
    {
        Param_Player.energy.Value = Param_Player.energy.Max;
    }

    void Start()
    {
        screenCon = ScreenController.instance;
        init = Initialize.instance;
        stageCon = StageController.instance;

        // 速度を参照するオブジェクトのrigidbodyを取得
        rb = this.GetComponent<Rigidbody>();

        // デリゲートに初期化関数を登録
        init.init_Stage += Init;
    }

    void Update()
    {
        // ゲーム画面でエネルギーが0になったらゲームオーバー処理
        if((screenCon.Screen == ScreenController.ScreenType.InGame) && (Param_Player.energy.Value <= 0))
        {
            stageCon.gameOverDele?.Invoke();
        }
    }
}
