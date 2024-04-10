using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// エネルギーのUIを管理
public class EnergyUIController : Singleton<EnergyUIController>
{
    [SerializeField] private Image energyGauge;                 // エネルギーゲージの画像
    [SerializeField] private Image energyAfterImage;            // エネルギーゲージの残像の画像
    [SerializeField] private Text EnergyValue;                  // エネルギーゲージの残量のテキスト

    private EnergyController eneCon;

    void Start()
    {
        eneCon = EnergyController.instance;
    }

    private void Update()
    {
        Draw();
    }

    // エネルギーのUIを描画
    void Draw()
    {
        // エネルギーゲージの増減を描画
        energyGauge.fillAmount = (float)eneCon.energy.Value / (float)eneCon.energy.Max;

        if (energyAfterImage.fillAmount > (float)eneCon.energy.Value / (float)eneCon.energy.Max)
        {
            // エネルギーゲージの減少量を少しずつ減らす
            energyAfterImage.fillAmount -=
                (energyAfterImage.fillAmount - (float)eneCon.energy.Value / (float)eneCon.energy.Max) * Time.deltaTime;
        }

        // エネルギーの数値を表示
        EnergyValue.text = eneCon.energy.Value.ToString("0");
    }
}
