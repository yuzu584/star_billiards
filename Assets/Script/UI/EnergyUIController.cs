using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

// エネルギーのUIを管理
public class EnergyUIController : MonoBehaviour
{
    [SerializeField] private EnergyController energyController; // InspectorでEnergyControllerを指定
    [SerializeField] private Initialize initialize;             // InspectorでInitializeを指定
    [SerializeField] private Image energyGauge;                 // エネルギーゲージの画像
    [SerializeField] private Image energyAfterImage;            // エネルギーゲージの残像の画像

    // エネルギーのUIを描画
    public void DrawEnergyUI(Image energyGaugeOutline, Text EnergyValue, Text NoEnergy)
    {
        // エネルギーゲージの増減を描画
        energyGauge.fillAmount = energyController.energy / energyController.maxEnergy;

        if (energyAfterImage.fillAmount > energyController.energy / energyController.maxEnergy)
        {
            // エネルギーゲージの減少量を少しずつ減らす
            energyAfterImage.fillAmount -=
                (energyAfterImage.fillAmount - energyController.energy / energyController.maxEnergy) * Time.deltaTime;
        }

        // エネルギーの数値を表示
        EnergyValue.text = energyController.energy.ToString("0");

        // エネルギーが0以下かつ非表示なら
        if ((energyController.energy <= 0) && (NoEnergy.enabled == false))
        {
            // エネルギーゲージの枠と数値を赤色にする
            energyGaugeOutline.color = new Color32(155, 0, 0, 100);
            EnergyValue.color = new Color32(155, 0, 0, 100);

            // エネルギーがない旨を伝えるテキストを表示
            NoEnergy.enabled = true;
        }
        // エネルギーが0より上かつ表示されているなら
        else if ((energyController.energy > 0) && (NoEnergy.enabled == true))
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

    void Start()
    {
        // デリゲートに初期化関数を登録
        initialize.init_Stage += Init;
    }
}
