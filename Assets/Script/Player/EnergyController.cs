using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// エネルギーの増減を管理
public class EnergyController : Singleton<EnergyController>
{
    public ClampedValue<int> energy;        // エネルギー
    public ClampedValue<int> maxEnergy;     // 最大エネルギー

    private ScreenController screenCon;
    private Initialize init;
    private StageController stageCon;

    // 初期化処理
    void Init()
    {
        energy.SetValue(energy.GetMax());
    }

    void Start()
    {
        screenCon = ScreenController.instance;
        init = Initialize.instance;
        stageCon = StageController.instance;

        energy = new ClampedValue<int>(1000, 1000, 0, nameof(energy));
        maxEnergy = new ClampedValue<int>(1000, 10000, 0, nameof(maxEnergy));

        // "最大エネルギー" の値が変化したときに "エネルギー" の最大値を設定
        maxEnergy.SetOnValueChanged(() => { energy.SetMax(maxEnergy.GetValue_Int()); });

        // デリゲートに初期化関数を登録
        init.init_Stage += Init;
    }

    void Update()
    {
        // ゲーム画面でエネルギーが0になったらゲームオーバー処理
        if((screenCon.Screen == ScreenController.ScreenType.InGame) && (energy.GetValue_Float() <= 0))
        {
            stageCon.gameOverDele?.Invoke();
        }
    }
}
