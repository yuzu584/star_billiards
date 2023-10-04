using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject chargeUI;       // チャージのUI
    public Text chargeValue;          // チャージの数値
    public Image chargeCircle;        // チャージの円
    public Text chargeName;           // チャージの文字
    public Image EnergyGauge;         // エネルギーゲージ
    public Image EnergyGaugeOutline;  // エネルギーゲージの枠
    public Text NoEnergy;             // エネルギーがない旨を伝えるテキスト
    public Text EnergyValue;             // エネルギーの数値

    void Start()
    {
        // エネルギーがない旨を伝えるテキストを非表示
        NoEnergy.enabled = false;
    }

    void Update()
    {
        // エネルギーゲージの増減を描画
        EnergyGauge.fillAmount = EnergyController.energy / EnergyController.maxEnergy;

        // エネルギーの数値を表示
        EnergyValue.text = EnergyController.energy.ToString("0");

        // チャージされているなら
        if (Shot.charge > 0)
        {
            // UIを有効化
            chargeUI.SetActive(true);

            // チャージの数値をテキストで表示
            chargeValue.text = Shot.charge.ToString("0") + "%";

            // チャージの円を描写
            chargeCircle.fillAmount = Shot.charge / 100;
        }
        // チャージされていないなら
        else if (Shot.charge == 0)
        {
            // UIを無効化
            chargeUI.SetActive(false);
        }

        // エネルギーが0以下なら
        if(EnergyController.energy <= 0)
        {
            // エネルギーゲージの枠を赤色にする
            EnergyGaugeOutline.color = new Color32(155, 0, 0, 100);

            // エネルギーがない旨を伝えるテキストを表示
            NoEnergy.enabled = true;
        }
        else
        {
            // エネルギーゲージの枠を白色にする
            EnergyGaugeOutline.color = new Color32(255, 255, 255, 100);

            // エネルギーがない旨を伝えるテキストを非表示
            NoEnergy.enabled = false;
        }
    }
}
