using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// エネルギーのUIを管理
public class EnergyUIController : Singleton<EnergyUIController>
{
    [SerializeField] private Image energyGauge;                 // エネルギーゲージの画像
    [SerializeField] private Image energyAfterImage;            // エネルギーゲージの残像の画像

    private EnergyController eneCon;
    private Initialize init;

    void Start()
    {
        eneCon = EnergyController.instance;
        init = Initialize.instance;

        // デリゲートに初期化関数を登録
        init.init_Stage += Init;
    }

    // エネルギーのUIを描画
    public void DrawEnergyUI(Image energyGaugeOutline, Text EnergyValue, Text NoEnergy)
    {
        // エネルギーゲージの増減を描画
        energyGauge.fillAmount = eneCon.energy / eneCon.maxEnergy;

        if (energyAfterImage.fillAmount > eneCon.energy / eneCon.maxEnergy)
        {
            // エネルギーゲージの減少量を少しずつ減らす
            energyAfterImage.fillAmount -=
                (energyAfterImage.fillAmount - eneCon.energy / eneCon.maxEnergy) * Time.deltaTime;
        }

        // エネルギーの数値を表示
        EnergyValue.text = eneCon.energy.ToString("0");

        // エネルギーが0以下かつ非表示なら
        if ((eneCon.energy <= 0) && (NoEnergy.enabled == false))
        {
            // エネルギーゲージの枠と数値を赤色にする
            energyGaugeOutline.color = new Color32(155, 0, 0, 100);
            EnergyValue.color = new Color32(155, 0, 0, 100);

            // エネルギーがない旨を伝えるテキストを表示
            NoEnergy.enabled = true;
        }
        // エネルギーが0より上かつ表示されているなら
        else if ((eneCon.energy > 0) && (NoEnergy.enabled == true))
        {
            // エネルギーゲージの枠と数値を白色にする
            energyGaugeOutline.color = new Color32(255, 255, 255, 100);
            EnergyValue.color = new Color32(255, 255, 255, 255);

            // エネルギーがない旨を伝えるテキストを非表示
            NoEnergy.enabled = false;
        }
    }

    // エネルギーのUIを初期化
    void Init()
    {
        energyGauge.fillAmount = 1;
        energyAfterImage.fillAmount = 1;
    }
}
