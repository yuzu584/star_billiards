using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// エネルギーの増減を管理
public class EnergyController : Singleton<EnergyController>
{
    private ScreenController screenCon;
    private Initialize init;
    private StageController stageCon;

    private Rigidbody rb;           // リジッドボディ

    public float energy = 1000;     // プレイヤーのエネルギー
    public float maxEnergy = 1000;  // 最大エネルギー

    // 初期化処理
    void Init()
    {
        energy = maxEnergy;
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
        if((screenCon.Screen == ScreenController.ScreenType.InGame) && (energy <= 0))
        {
            stageCon.gameOverDele?.Invoke();
        }

        // エネルギーの数値が範囲外なら範囲内に戻す
        if (energy > maxEnergy)
            energy = maxEnergy;
        else if (energy < 0)
            energy = 0;
    }
}
