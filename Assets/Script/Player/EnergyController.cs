using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// エネルギーの増減を管理
public class EnergyController : MonoBehaviour
{
    [SerializeField] private ScreenController screenController; // InspectorでScreenControllerを指定
    [SerializeField] private GameOver gameOver;                 // InspectorでGameOverを指定
    [SerializeField] private Initialize initialize;             // InspectorでInitializeを指定

    public float energy = 1000;     // プレイヤーのエネルギー
    public float maxEnergy = 1000;  // 最大エネルギー
    private Rigidbody rb;           // リジッドボディ

    // 初期化処理
    void Init()
    {
        energy = maxEnergy;
    }

    void Start()
    {
        // 速度を参照するオブジェクトのrigidbodyを取得
        rb = this.GetComponent<Rigidbody>();

        // デリゲートに初期化関数を登録
        initialize.init_Stage += Init;
    }

    void Update()
    {
        // ゲーム画面でエネルギーが0になったらゲームオーバー処理
        if((screenController.ScreenNum == 5) && (energy <= 0))
        {
            gameOver.GameOverProcess();
        }

        // エネルギーの数値が範囲外なら範囲内に戻す
        if (energy > maxEnergy)
            energy = maxEnergy;
        else if (energy < 0)
            energy = 0;
    }
}
