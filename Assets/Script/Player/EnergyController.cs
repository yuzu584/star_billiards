using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// エネルギーの増減を管理
public class EnergyController : Singleton<EnergyController>
{
    public ClampedValue<int> energy = new ClampedValue<int> (1000, 1000, 0);    // エネルギー

    private ScreenController screenCon;
    private Initialize init;
    private StageController stageCon;

    private Rigidbody rb;                                                       // リジッドボディ

    // 初期化処理
    void Init()
    {
        energy.Value = energy.Max;
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
        if((screenCon.Screen == ScreenController.ScreenType.InGame) && (energy.Value <= 0))
        {
            stageCon.gameOverDele?.Invoke();
        }
    }
}
